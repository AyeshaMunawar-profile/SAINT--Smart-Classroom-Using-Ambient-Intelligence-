using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAINTClicker.Models
{
    [JsonObject(Title = "Quiz")]
    public class Quiz
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("regNo")]
        public string regNo { get; set; }

        [JsonProperty("myClass")]
        public string myClass { get; set; }

        [JsonProperty("myCourse")]
        public string myCourse { get; set; }

        [JsonProperty("myLectureNo")]
        public string myLectureNo { get; set; }

        [JsonProperty("questionid")]
        public string questionid { get; set; }

        [JsonProperty("questionName")]
        public string questionName { get; set; }

        [JsonProperty("option1")]
        public string option1 { get; set; }

        [JsonProperty("option2")]
        public string option2 { get; set; }

        [JsonProperty("option3")]
        public string option3 { get; set; }

        [JsonProperty("option4")]
        public string option4 { get; set; }
    }
}