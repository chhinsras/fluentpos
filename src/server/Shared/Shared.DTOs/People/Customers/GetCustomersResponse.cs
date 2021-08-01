using System;

namespace FluentPOS.Shared.DTOs.People.Customers
{
    public record GetCustomersResponse(Guid Id, string Name, string Phone, string Email, string ImageUrl, string Type);
}