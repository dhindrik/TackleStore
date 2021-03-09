using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Search;
using System.Linq;
using TackleStore.Models;

namespace Functions
{
    public static class SearchProducts
    {
        [FunctionName("SearchProducts")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {          
            string searchText = req.Query["text"];

            var searchKey = Environment.GetEnvironmentVariable("SearchKey");
            var searchInstanceName = Environment.GetEnvironmentVariable("SearchInstanceName");

            // var searchClient = new SearchServiceClient(searchInstanceName, new SearchCredentials(searchKey));

            var searchClient = new SearchIndexClient(searchInstanceName, ProductIndexer.SearchIndexName, new SearchCredentials(searchKey));

            var response = await searchClient.Documents.SearchAsync<ProductIndexModel>(searchText);

            var result = response.Results.Select(x => new SearchResultItem()
            {
             Id = x.Document.Id,
             Image = x.Document.Image,
             Price = x.Document.Price,
             Title = x.Document.Title
            }).ToList();

            return new OkObjectResult(result);
        }
    }
}
