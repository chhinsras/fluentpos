using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;
using System;

namespace FluentPOS.Modules.People.Core.Features.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }
}