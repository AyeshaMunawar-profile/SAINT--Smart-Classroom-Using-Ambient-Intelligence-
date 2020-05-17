using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace SAINTClicker.Models
{
    [JsonObject(Title = "User")]
    public class User
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("registrationNo")]
        public string registrationNo { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }


        [JsonProperty("role")]
        public string role { get; set; }

        [JsonProperty("myclass")]
        public string myclass { get; set; }

        [JsonProperty("mycourse")]
        public string mycourse { get; set; }

        [UpdatedAt]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
