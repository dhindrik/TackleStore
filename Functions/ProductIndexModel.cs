using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Cosmos.Core.Collections;
using Microsoft.Azure.Search;

namespace Functions
{
    public record ProductIndexModel
    {
        [Key]
        [IsSearchable, IsRetrievable(true)]
        public string Id { get; set; }
        [IsSearchable, IsRetrievable(true)]
        public string Title { get; set; } = string.Empty;
        [IsSearchable, IsRetrievable(true)]
        public string? Body { get; set; }
        [IsSearchable, IsRetrievable(false)]
        public string CollectionNames { get; set; }
    }
}
