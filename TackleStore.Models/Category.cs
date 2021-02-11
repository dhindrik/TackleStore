using System;
using System.Collections.Generic;

namespace TackleStore.Models
{
    public record Category
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public int ParentId { get; set; }
        public List<Category> SubCategories { get; init; } = new List<Category>();
    }
}
