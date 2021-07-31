using System.Collections.Generic;
using System.Net;
using FluentPOS.Shared.Core.Exceptions;

namespace FluentPOS.Modules.People.Core.Exceptions
{
    public class CartNotFoundException : CustomException
    {
        public CartNotFoundException() : base("Cart Not Found", null, HttpStatusCode.NotFound)
        {
        }
    }
}