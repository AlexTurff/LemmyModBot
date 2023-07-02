using LemmyModBot.RequestModels;
using LemmyModBot.ResponseModels;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Websocket.Client;

namespace LemmyModBot
{
    public class WebsocketApiConnection : IDisposable, IApiConnection
    {
        public WebsocketApiConnection(string url)
        {
            Url = url;
        }

        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public Queue<string> RecievedQueue = new Queue<string>();
        public string Url { get; }
        private WebsocketClient Connection { get; set; }
        private string JwtToken { get; set; }



        public void Login(string username, string password)
        {
            using (WebsocketClient client = new WebsocketClient(new Uri(Url)))
            {
                client.MessageReceived.Subscribe(msg =>
                {
                    Logger.Trace($"Login Connection message received (not logged - duh");
                    RecievedQueue.Enqueue(msg.Text);
                });

                client.Start().Wait();

                var message = JsonSerializer.Serialize(new ApiOperation<LoginRequest>() { Operation = LoginRequest.OperationName, Data = new LoginRequest(username, password) });
                client.Send(message);

                string jwtToken = null;

                while (jwtToken == null)
                {
                    string response;
                    if (RecievedQueue.TryDequeue(out response))
                    {
                        var responseObject = JsonSerializer.Deserialize<ApiOperation<LoginResponse>>(response);
                        jwtToken = responseObject.Data.JwtToken;
                    }
                }

                JwtToken = jwtToken;

                Connect();
            }
        }

        public TResponse SendRequest<TRequest, TResponse>(ApiOperation<TRequest> request) where TRequest : RequestBase, new()
        {
            request.Data.Jwt = JwtToken;
            var message = JsonSerializer.Serialize(request);

            Connection.Send(message);
            ApiOperation<TResponse> response = null;

            //todo - this will only work in a sequential mode (if the response is always for the previous request)
            while (response == null)
            {
                string responseText = null;
                if (RecievedQueue.TryDequeue(out responseText))
                {
                    var responseObject = JsonSerializer.Deserialize<ApiOperation<TResponse>>(responseText);
                    response = responseObject;
                }
            }

            return response.Data;
        }

        private void Connect()
        {
            WebsocketClient client = new WebsocketClient(new Uri(Url));
            client.ReconnectTimeout = TimeSpan.FromSeconds(30);
            client.ReconnectionHappened.Subscribe(info => Logger.Info($"Reconnection happened, type: {info.Type}"));
            client.MessageReceived.Subscribe(msg =>
            {
                //todo - how to avoid logging JWT tokens?
                Logger.Trace($"Message received: {msg}");
                RecievedQueue.Enqueue(msg.Text);
            });

            client.Start().Wait();

            Connection = client;
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
