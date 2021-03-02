using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Search;
using TackleStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Search.Models;

namespace Functions
{
    public static class RebuildSearchIndex
    {
        private const string SearchIndexName = "products";

        [FunctionName("RebuildSearchIndex")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            var connestionString = Environment.GetEnvironmentVariable("CosmosDBConnection");
            

            var client = new CosmosClient(connestionString);
            var database = await client.CreateDatabaseIfNotExistsAsync("products");
            var productContainerResponse = await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties("products", "/_Partion"));
            
            var productIterator = productContainerResponse.Container.GetItemLinqQueryable<ProductItem>().ToFeedIterator();


            var products = new List<ProductItem>();

            while (productIterator.HasMoreResults)
            {
                foreach(var product in await productIterator.ReadNextAsync())
                {
                    products.Add(product);
                }
            }

            var indexer = new ProductIndexer();
            await indexer.Index(products, true);

            return new OkResult();
        }
    }
}
