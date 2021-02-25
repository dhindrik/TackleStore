using System;
namespace Functions
{
    public record ProductIndexModel
    {
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
        public string CollectionNames { get; set; }
    }
}
