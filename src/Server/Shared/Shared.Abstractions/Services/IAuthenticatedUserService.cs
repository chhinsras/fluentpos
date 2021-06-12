using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Abstractions.Services
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
