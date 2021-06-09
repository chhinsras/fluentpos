using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Shared.Abstractions.Interfaces.Services;
using FluentPOS.Shared.Abstractions.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands.AddEdit
{
    public partial class AddEditBrandCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Detail { get; set; }

        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditBrandCommandHandler : IRequestHandler<AddEditBrandCommand, Result<Guid>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditBrandCommandHandler> _localizer;

        public AddEditBrandCommandHandler(ICatalogDbContext context, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditBrandCommandHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(AddEditBrandCommand command, CancellationToken cancellationToken)
        {
            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"B-{command.Name}{uploadRequest.Extension}";
            }

            if (command.Id == Guid.Empty)
            {
                var brand = _mapper.Map<Brand>(command);
                if (uploadRequest != null)
                {
                    brand.ImageUrl = _uploadService.UploadAsync(uploadRequest);
                }
                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(brand.Id, _localizer["Brand Saved"]);
            }
            else
            {
                var brand = await _context.Brands.SingleOrDefaultAsync(b => b.Id == command.Id);
                if (brand != null)
                {
                    brand.Name = command.Name ?? brand.Name;
                    if (uploadRequest != null)
                    {
                        brand.ImageUrl = _uploadService.UploadAsync(uploadRequest);
                    }
                    brand.ImageUrl = command.ImageUrl ?? brand.ImageUrl;
                    brand.Detail = command.Detail ?? brand.Detail;
                    _context.Brands.Update(brand);
                    return await Result<Guid>.SuccessAsync(brand.Id, _localizer["Brand Updated"]);
                }
                else
                {
                    return await Result<Guid>.FailAsync(_localizer["Brand Not Found!"]);
                }
            }
        }
    }
}