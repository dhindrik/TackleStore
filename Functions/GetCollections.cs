using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Backend.Core;

namespace Functions
{
    public class GetCollections
    {
        private readonly ICollectionsRepository collectionsRepository;

        public GetCollections(ICollectionsRepository collectionsRepository)
        {
            this.collectionsRepository = collectionsRepository;
        }

        [FunctionName("GetCollections")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var collections = await collectionsRepository.Get();

            return new OkObjectResult(collections);
        }
    }
}
