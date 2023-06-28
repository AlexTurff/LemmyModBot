using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot.ResponseModels
{
    public class LoginResponse
    {
        [JsonPropertyName("jwt")]
        public string JwtToken { get; set; }

        [JsonPropertyName("registration_created")]
        public bool RegistrationCreated { get; set; }

        [JsonPropertyName("verify_email_sent")]
        public bool VerifyEmailSent { get; set; }
    }
}
