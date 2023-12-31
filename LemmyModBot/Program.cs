﻿using System.Net.WebSockets;
using System.Net;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Text;
using Websocket.Client;
using LemmyModBot.RequestModels;
using LemmyModBot.ResponseModels;
using LemmyModBot.ModerationTasks;
using LemmyModBot.ModerationTasks.ModerationActions;
using NLog;

namespace LemmyModBot
{
    internal class Program
    {
        //todo replace these with proper dependency injection framework?
        public static ModerationActionFactory ModerationActionFactory { get; set; }
        public static ModerationTaskFactory ModerationTaskFactory { get; set; }

        private static Logger Logger = LogManager.GetCurrentClassLogger();


        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Logger.Info("Starting");

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Configuration.Sources.Clear();

            IHostEnvironment env = builder.Environment;

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddUserSecrets<Program>();

            using IHost host = builder.Build();

            host.RunAsync();

            var lemmyUrl = builder.Configuration.GetValue<string>("LemmyApiHost");
            var lemmyUser = builder.Configuration.GetValue<string>("LemmyUsername");
            var lemmyPassword = builder.Configuration.GetValue<string>("LemmyPassword");

            var connection = new HttpApiConnection(lemmyUrl);
            connection.Login(lemmyUser, lemmyPassword);
            var lastCredentialRefresh = DateTime.UtcNow;

            ModerationTaskFactory = new ModerationTaskFactory(builder.Configuration);
            ModerationActionFactory = new ModerationActionFactory(connection);

            var moderationTasks = ModerationTaskFactory.GetModerationTasks();
            var moderationRunner = new ModerationRunner(connection, moderationTasks);

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            var pollingDelay = builder.Configuration.GetValue<int>("PollDelaySeconds");
            var refreshCredentialMinutes = builder.Configuration.GetValue<int>("RefreshCredentialMinutes");

            while (!cancellationToken.IsCancellationRequested)
            {
                var startTime = DateTime.UtcNow;

                if(lastCredentialRefresh.AddMinutes(refreshCredentialMinutes) < DateTime.UtcNow)
                {
                    connection.Login(lemmyUser, lemmyPassword);
                    lastCredentialRefresh = DateTime.UtcNow;
                }

                moderationRunner.Run();

                var endTime = DateTime.UtcNow;
                var secondsSpent = (endTime - startTime).TotalSeconds;
                Thread.Sleep(pollingDelay*1000);
            }

            Logger.Info("Stopping");
            Console.WriteLine("Goodbye, World!");
        }       
    }
}