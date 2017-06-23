using System;

using Newtonsoft.Json;

namespace SeidorDemo
{
    public class Document
    {
        [JsonProperty("documentName")]
        public string Name { get; set; }

        [JsonProperty("documentURL")]
        public string Url  { get; set; }

        [JsonIgnore]
        public string ImageFilename
        {
            get { return "ic_file"; }
        }
    }
}