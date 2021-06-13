using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands
{
    public class RegisterBrandCommand : BrandCommand, IRequest<Result<Guid>>
    {
        public RegisterBrandCommand(string name, string imageUrl, string detail, UploadRequest uploadRequest)
        {
            Name = name;
            ImageUrl = imageUrl;
            Detail = detail;
            UploadRequest = uploadRequest;
        }
    }
}