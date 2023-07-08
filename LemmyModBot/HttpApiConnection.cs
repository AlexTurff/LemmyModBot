using LemmyModBot.RequestModels;
using LemmyModBot.ResponseModels;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using Websocket.Client;

namespace LemmyModBot
{
    public class HttpApiConnection : IDisposable, IApiConnection
    {
        public HttpApiConnection(string url)
        {
            Url = url;
            HttpClient = new HttpClient();
        }

        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public string Url { get; }
        public string Username { get; private set; }
        private HttpClient HttpClient { get; }
        private string JwtToken { get; set; }



        public void Login(string username, string password)
        {
            Username = username;
            var message = new HttpRequestMessage(HttpMethod.Post, new Uri(Url.TrimEnd('/') +"/user/login"));
            message.Content = JsonContent.Create(new LoginRequest(username, password),typeof(LoginRequest));
            
            var response = HttpClient.Send(message);

            if (response != null && response.IsSuccessStatusCode && response.Content != null)
            {
                var readResponseTask = response.Content.ReadAsStringAsync();
                readResponseTask.Wait();
                var responseObject = JsonSerializer.Deserialize<LoginResponse>(readResponseTask.Result);

                if (responseObject != null && !string.IsNullOrWhiteSpace(responseObject.JwtToken)) 
                {
                    JwtToken = responseObject.JwtToken;
                }
                else { throw new Exception("Failed to Login - could not get Token"); }
            }
            else { throw new Exception("Failed to Login - " + response.StatusCode); }
        }

        public TResponse? SendRequest<TRequest,TResponse>(TRequest request) where TRequest : RequestBase
        {
            try
            {
                request.Jwt = JwtToken;

                var message = new HttpRequestMessage(request.Operation, new Uri(Url.TrimEnd('/') + request.OperationRoute));

                if (request.Operation == HttpMethod.Get)
                {
                    var urlEncodedString = string.Join("&", typeof(TRequest).GetProperties()
                        .Where(p => p.GetCustomAttribute<JsonPropertyNameAttribute>() != null)
                        .Select(property => property.GetCustomAttribute<JsonPropertyNameAttribute>().Name + "=" + HttpUtility.UrlEncode(property.GetValue(request).ToString()))
                        .ToArray());
                    //

                    message.RequestUri = new Uri(message.RequestUri.AbsoluteUri + "?" + urlEncodedString);
                }
                else
                {
                    var messageText = JsonSerializer.Serialize<TRequest>(request);
                    message.Content = JsonContent.Create(request, typeof(TRequest));
                }

                //todo try catch me
                var response = HttpClient.Send(message);

                if (response != null && response.IsSuccessStatusCode && response.Content != null)
                {
                    var readResponseTask = response.Content.ReadAsStringAsync();
                    readResponseTask.Wait();
                    var responseObject = JsonSerializer.Deserialize<TResponse>(readResponseTask.Result);

                    return responseObject;
                }
                //todo logging
                else { throw new Exception("Error - " + response.StatusCode + " " + response.RequestMessage + " " + response.Content); }
            }
            catch (Exception ex) { 
                Logger.Error(ex);
                return default;
            }        
        }       

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
