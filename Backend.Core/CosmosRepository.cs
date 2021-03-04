using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Backend.Core
{
    public abstract class CosmosRepository
    {
        private Container container;

        protected async Task<Container> GetContainer(string collectionName, string partitionKey)
        {
            if (container is not null)
            {
                return container;
            }

            var connestionString = Environment.GetEnvironmentVariable("CosmosDBConnection");

            var client = new CosmosClient(connestionString);
            var database = await client.CreateDatabaseIfNotExistsAsync("products");

            var containerResponse = await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties(collectionName, partitionKey));

            container = containerResponse.Container;


            return container;
        }
    }
}
