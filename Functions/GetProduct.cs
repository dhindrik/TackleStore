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
    public class GetProduct
    {
        private readonly IProductRepository productRepository;

        public GetProduct(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [FunctionName("GetProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {          

            string id = req.Query["id"];

            var product = await productRepository.Get(id);

            if(product is null)
            {
                return new NotFoundResult();
            }
          

            return new OkObjectResult(product);
        }
    }
}
