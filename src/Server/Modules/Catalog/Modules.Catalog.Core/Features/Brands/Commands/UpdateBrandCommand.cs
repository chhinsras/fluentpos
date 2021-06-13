using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands
{
    public class UpdateBrandCommand : BrandCommand, IRequest<Result<Guid>>
    {
        public UpdateBrandCommand(Guid id, string name, string imageUrl, string detail, UploadRequest uploadRequest)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Detail = detail;
            UploadRequest = uploadRequest;
        }
    }
}