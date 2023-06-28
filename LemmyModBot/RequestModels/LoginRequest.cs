using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot.RequestModels
{
    public class LoginRequest
    {
        public LoginRequest() { }
        public LoginRequest(string user, string password) {
            UserIdentifier = user ?? throw new ArgumentNullException(nameof(user));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }


        [JsonIgnore]
        public static string OperationName = "Login";

        [JsonPropertyName("username_or_email")]
        public string UserIdentifier { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
