using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.People.Customers
{
    public record GetCustomerByIdResponse(Guid Id, string Name, string Phone, string Email, string ImageUrl, string Type);
}
