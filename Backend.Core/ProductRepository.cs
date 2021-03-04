using System;
using System.Threading.Tasks;
using TackleStore.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Backend.Core
{
    public class ProductRepository : CosmosRepository,  IProductRepository
    {
        


        public async Task<ProductItem?> Get(string id)
        {
            var container = await GetContainer("products", "/_Partion");
            var iterator = container.GetItemLinqQueryable<ProductItem>(true).Where(x => x.Id == id).ToFeedIterator();

            var products = new List<ProductItem>();

            while (iterator.HasMoreResults)
            {
                foreach (var product in await iterator.ReadNextAsync())
                {
                    products.Add(product);
                }
            }

            return products.SingleOrDefault();
        }
    }
}
