using System;
using Newtonsoft.Json;

namespace TackleStore.Models
{
    public class Collection
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class ImportCollection
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
