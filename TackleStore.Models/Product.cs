using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TackleStore.Models
{
    public record Product
    {
        public List<int> Categories { get; init; } = new List<int>();
        public string? Description { get; init; }
        public string Sku { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        public double Price { get; init; }
    }

    public record ProductImportItem
    {
        public List<long> CollectionId { get; set; } = new List<long>();
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        public List<Image> Images { get; set; } = new List<Image>();
    }

    public record ProductItem
    {
        public List<long> CollectionId { get; set; } = new List<long>();
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        public List<Image> Images { get; set; } = new List<Image>();
        public double Price { get; set; }
    }

    public record Variant
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
    }

    public record Option
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new List<string>();
    }

    public record Image
    {
        public long Id { get; set; }
        public string Src { get; set; } = string.Empty;
    }
}
