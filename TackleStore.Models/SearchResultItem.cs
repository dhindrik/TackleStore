using System;
using System.Security.Cryptography.X509Certificates;

namespace TackleStore.Models
{
    public class SearchResultItem
    {
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string? Image { get; set; }
    }
}
