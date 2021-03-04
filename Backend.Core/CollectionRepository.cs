using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Linq;
using TackleStore.Models;

namespace Backend.Core
{
    public class CollectionRepository : CosmosRepository, ICollectionsRepository
    {
        public CollectionRepository()
        {
        }

        public async Task<IEnumerable<Collection>> Get()
        {
            var container = await GetContainer("collections", "/_Partion");

            var collections = new List<Collection>();

            var iterator = container.GetItemLinqQueryable<Collection>().ToFeedIterator();

            while (iterator.HasMoreResults)
            {
                foreach (var collection in await iterator.ReadNextAsync())
                {
                    collections.Add(collection);
                }
            }

            return collections;
        }
    }
}
