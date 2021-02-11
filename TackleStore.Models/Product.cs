using System;
using System.Collections.Generic;

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
}
