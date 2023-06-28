using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot
{
    public class ApiOperation<TOperation>
    {

        [JsonPropertyName("op")]
        public string Operation { get; set; }
        [JsonPropertyName("data")]
        public TOperation Data { get; set; }
    }
}
