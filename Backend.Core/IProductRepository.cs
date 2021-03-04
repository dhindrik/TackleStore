using System;
using System.Threading.Tasks;
using TackleStore.Models;

namespace Backend.Core
{
    public interface IProductRepository
    {
        Task<ProductItem> Get(string id);
    }
}
