using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using TackleStore.Models;

namespace Functions
{
    public class ProductIndexer
    {
        public ProductIndexer(ICollectionsRepository collectionsRepository)
        {
            this.collectionsRepository = collectionsRepository;
        }

        public const string SearchIndexName = "products";
        private readonly ICollectionsRepository collectionsRepository;

        public async Task Index(IEnumerable<ProductItem> products, bool recreate = false)
        {

            var connestionString = Environment.GetEnvironmentVariable("CosmosDBConnection");

            var client = new CosmosClient(connestionString);
            var database = await client.CreateDatabaseIfNotExistsAsync("products");

            var collections = await collectionsRepository.Get();

            var searchKey = Environment.GetEnvironmentVariable("SearchKey");
            var searchInstanceName = Environment.GetEnvironmentVariable("SearchInstanceName");

            var searchClient = new SearchServiceClient(searchInstanceName, new SearchCredentials(searchKey));

            if(recreate)
            {
                searchClient.Indexes.Delete(SearchIndexName);
            }


            if (!searchClient.Indexes.Exists(SearchIndexName))
            {

                searchClient.Indexes.Create(new Microsoft.Azure.Search.Models.Index()
                {
                    Name = SearchIndexName,
                    Fields = FieldBuilder.BuildForType<ProductIndexModel>()
                });
            }

            var indexClient = searchClient.Indexes.GetClient(SearchIndexName);

            var indexModels = new List<IndexAction<ProductIndexModel>>();

            foreach (var product in products)
            {
                var ids = product.CollectionId.Select(x => x.ToString());

                var items = collections.Where(x => ids.Contains(x.Id)).Select(x => x.Title);

                var indexModel = new ProductIndexModel()
                {
                    Body = product.Body,
                    Id = product.Id,
                    Title = product.Title,
                    Image = product.Images.FirstOrDefault() != null ? product.Images.First().Src : null,
                    Price = product.Variants.OrderByDescending(x => x.Price).Select(x => x.Price).FirstOrDefault(),
                    CollectionNames = string.Join(' ', items)
                };

                indexModels.Add(IndexAction.Upload(indexModel));
            }

            await indexClient.Documents.IndexAsync(IndexBatch.New<ProductIndexModel>(indexModels));
        }
    }
}
