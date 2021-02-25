using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using TackleStore.Models;

var productJson = File.ReadAllText("ExportedProducts.json");

var products = JsonConvert.DeserializeObject<List<ProductImportItem>>(productJson);

Console.WriteLine($"Number of products: {products.Count}");

var collectionJson = File.ReadAllText("ExportedCollections.json");
var collections = JsonConvert.DeserializeObject<List<ImportCollection>>(collectionJson);

Console.WriteLine($"Number of collections: {collections.Count}");

var connestionString = "";

var client = new CosmosClient(connestionString);
var database = client.GetDatabase("products");
var productContainer = database.GetContainer("products");
var collectionsContainer = database.GetContainer("collections");

Task.Run(async () =>
{
    var productTasks = new List<Task>();

    var items = products.Select(x => new ProductItem()
    {
        Id = x.Id.ToString(),
        Body = x.Body,
        CollectionId = x.CollectionId,
        Images = x.Images,
        Options = x.Options,
        Title = x.Title
    });

    foreach (var product in items)
    {
        await productContainer.CreateItemAsync(product);
    }
   // await Task.WhenAll(productTasks);
});

Task.Run(async () =>
{
    var collectionTasks = new List<Task>();

    var items = collections.Select(x => new Collection()
    {
        Id = x.Id.ToString(),
        Title = x.Title
    });

    foreach (var collection in items)
    {
        await collectionsContainer.CreateItemAsync(collection);
    }

  // await Task.WhenAll(collectionTasks);

    Console.WriteLine("Collection import has finished");
});






Console.ReadLine();