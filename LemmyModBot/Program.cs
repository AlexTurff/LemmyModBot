using System.Net.WebSockets;
using System.Net;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Text;
using Websocket.Client;
using LemmyModBot.RequestModels;
using LemmyModBot.ResponseModels;

namespace LemmyModBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Configuration.Sources.Clear();

            IHostEnvironment env = builder.Environment;

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

            using IHost host = builder.Build();

            // Application code should start here.

            host.RunAsync();

            var lemmyUrl = builder.Configuration.GetValue<string>("LemmyApiHost");

            var connection = new ApiConnection(lemmyUrl);

            var lemmyUser = builder.Configuration.GetValue<string>("LemmyUsername");
            var lemmyPassword = builder.Configuration.GetValue<string>("LemmyPassword");

            connection.Login(lemmyUser, lemmyPassword);

            var response = connection.SendRequest<GetPostsRequest, GetPostsResponse>(
                new ApiOperation<GetPostsRequest>() 
                    { Operation = GetPostsRequest.OperationName, Data = new GetPostsRequest("imaginarycosmere", 1) });    

 
            Console.WriteLine("Goodbye, World!");

        }



        

    }
}