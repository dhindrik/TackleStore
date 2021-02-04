using System;
using System.Collections.Generic;

namespace TackleStore.Models
{
    public abstract record ProductBase
    {
        public string Sku { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
    }

    public abstract record Product : ProductBase
    {
        public List<int> Categories { get; init; } = new List<int>();
        public string? Description { get; init; }
    }

    public record SingleProduct : Product
    {  
        public double Price { get; init; } 
    }

    public record VariantProduct : Product
    { 
        public Dictionary<string, List<Variant>> Variants = new Dictionary<string, List<Variant>>();
    }

    public record Variant : Product
    {
        public double Price { get; init; }
        
    }
}
