using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAINTClicker.Models
{
    [JsonObject(Title = "Responses")]
    public class Responses
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("myCourse")]
        public string myCourse { get; set; }

        [JsonProperty("myClass")]
        public string myClass { get; set; }


        [JsonProperty("questionid")]
        public string questionid { get; set; }

        [JsonProperty("questionName")]
        public string questionName { get; set; }

        [JsonProperty("answerIndex")]
        public string answerIndex { get; set; }

        [JsonProperty("answerOption")]
        public string answerOption { get; set; }

        [JsonProperty("myLectureNo")]
        public string myLectureNo { get; set; }
        

    }
}
