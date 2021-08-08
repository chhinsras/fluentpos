using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.Sales.Core.Features.Sales.Commands
{
    //Will Return back the Order Id
    public class RegisterSaleCommand : IRequest<Result<Guid>>
    {
        //From CartId
        //Get Customer ID, Use Intergration Services to Get Customer Details
        //Get CartItem Details, Use Intergration Services to Get Product Details
        //Calculate Tax, Total
        //Save to Sales.Order,Transactions and Product
        //Delete CartItem and Cart 
        public Guid CartId { get; set; }
    }
}