using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TackleStore.Models;

namespace Backend.Core
{
    public interface ICollectionsRepository
    {
        Task<IEnumerable<Collection>> Get();
    }
}
