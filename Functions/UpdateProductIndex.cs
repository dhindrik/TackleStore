using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TackleStore.Models;

namespace Functions
{
    public class UpdateProductIndex
    {
        private readonly ProductIndexer productIndexer;

        public UpdateProductIndex(ProductIndexer productIndexer)
        {
            this.productIndexer = productIndexer;
        }

        [FunctionName("UpdateProductIndex")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "products",
            collectionName: "products",
            ConnectionStringSetting = "CosmosDBConnection",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                var products = input.Select(x => (ProductItem)(dynamic)x);

                if (products is not null)
                {
                    await productIndexer.Index(products);
                }
            }
        }
    }
}
