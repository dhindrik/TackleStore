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
using Microsoft.Azure.Search;
using TackleStore.Models;
using System.Collections.Generic;

namespace Functions
{
    public static class Functions
    {
        [FunctionName("Functions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            var connestionString = Environment.GetEnvironmentVariable("CosmosDBConnection");
            var searchKey = Environment.GetEnvironmentVariable("SearchKey");
            var searchInstanceName = Environment.GetEnvironmentVariable("SearchInstanceName");

            var client = new CosmosClient(connestionString);
            var database = await client.CreateDatabaseIfNotExistsAsync("products");
            var productContainer = await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties("products", "/Partition"));
            var collectionsContainer = await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties("collections", "/Partition"));

            var searchClient = new SearchServiceClient(searchInstanceName, new SearchCredentials(searchKey));

            var query = new QueryDefinition("select * from c");

            //var iterator = collectionsContainer.Container.<Collection>(query);

            //if(iterator is not null)
            //{
            //    await foreach (var item in iterator)
            //    {

            //    }
            //}
            

            //var collections = new List<Collection>();

            //while(iterator.HasMoreResults)
            //{
            //    var response = await iterator.ReadNextAsync();

            //    foreach(var item in response)
            //    {
            //        collections.Add(item);
            //    }
            //}

    

            return new OkResult();
        }
    }
}
