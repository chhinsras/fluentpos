using FluentPOS.Shared.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Exceptions
{
    public class PeopleException : CustomException
    {
        public PeopleException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, statusCode: statusCode)
        {
        }
    }
}
