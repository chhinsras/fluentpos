using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Constants
{
    public static class PeopleCacheKeys
    {
        public static string GetCustomerByIdCacheKey(Guid id)
        {
            return $"Customer-{id}";
        }
    }
}
