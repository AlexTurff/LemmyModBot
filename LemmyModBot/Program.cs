using System.Net.WebSockets;
using System.Net;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Text;
using Websocket.Client;

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
            CancellationToken cancellationToken = new CancellationToken();

            var connectionTask = Connect(lemmyUrl, cancellationToken);
            connectionTask.Wait();
            var connection = connectionTask.Result;

            var lemmyUser = builder.Configuration.GetValue<string>("LemmyUsername");
            var lemmyPassword = builder.Configuration.GetValue<string>("LemmyPassword");

            var loginTask = Login(connection, lemmyUser, lemmyPassword);
            loginTask.Wait();
            var authToken = loginTask.Result;


            connection.Dispose();
            Console.WriteLine("Goodbye, World!");

        }

        //public static async Task<ClientWebSocket> Connect(string url,CancellationToken cancellationToken)
        //{
        //    using SocketsHttpHandler handler = new();
        //    using ClientWebSocket ws = new();

        //    ws.Options.HttpVersion = HttpVersion.Version11;
        //    ws.Options.HttpVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;

        //    await ws.ConnectAsync(new Uri(url), new HttpMessageInvoker(handler), cancellationToken);            

        //    return ws;
        //}

        //public static async Task<ClientWebSocket> Connect(string url, CancellationToken cancellationToken)
        //{
        //    SocketsHttpHandler handler = new();
        //    ClientWebSocket ws = new();

        //    ws.Options.HttpVersion = HttpVersion.Version11;
        //    ws.Options.HttpVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        //    ws.Options.KeepAliveInterval = new TimeSpan(0, 10, 0);
        //    ws.Options.CollectHttpResponseDetails = true;

        //    await ws.ConnectAsync(new Uri(url), new HttpMessageInvoker(handler), cancellationToken);

        //    return ws;
        //}

        public static Queue<string> RecievedQueue = new Queue<string>();

        public static async Task<WebsocketClient> Connect(string url, CancellationToken cancellationToken)
        {
            SocketsHttpHandler handler = new();
            WebsocketClient client = new WebsocketClient(new Uri(url));
            client.ReconnectTimeout = TimeSpan.FromSeconds(30);
            client.ReconnectionHappened.Subscribe(info => Console.WriteLine($"Reconnection happened, type: {info.Type}"));
            client.MessageReceived.Subscribe(msg => {
                Console.WriteLine($"Message received: {msg}");
                RecievedQueue.Enqueue(msg.Text);        
            });

            await client.Start();

            return client;
        }

        public static async Task<string> Login(WebsocketClient connection, string username, string password)
        {
            return await Task.Run<string>(() => {
                var messageString = $"{{ \"op\":\"Login\",\"data\":{{ \"username_or_email\":\"{username}\",\"password\":\"{password}\"}} }}";
                var cancel = new CancellationToken();
                var buffer = new Memory<byte>();

                connection.Send(messageString);
                string token = null;

                while(token == null) 
                { 
                    RecievedQueue.TryDequeue(out token);
                }
                //todo - extract token from json
                return token;
            });        
        }

        //public static async Task<string> Login(ClientWebSocket connection, string username, string password)
        //{
        //    var messageString = $"{{ \"op\":\"Login\",\"data\":{{ \"username_or_email\":\"{username}\",\"password\":\"{password}\"}} }}";
        //    var cancel = new CancellationToken();
        //    var buffer = new Memory<byte>();

        //    connection.SendAsync(Encoding.UTF8.GetBytes(messageString), WebSocketMessageType.Text, true, cancel);
        //    await connection.ReceiveAsync(buffer, cancel);

        //    return Encoding.UTF8.GetString(buffer.ToArray());

        //}
    }
}