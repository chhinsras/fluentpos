using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Constants;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Application.Interfaces.Services;
using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    public partial class EditProductCommand : IRequest<Result<Guid>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LocaleName { get; set; }
        [Required]
        public Guid BrandId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public string Tax { get; set; }
        public string TaxMethod { get; set; }
        [Required]
        public string BarcodeSymbology { get; set; }
        public bool IsAlert { get; set; }
        public decimal AlertQuantity { get; set; }
        public string Detail { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class EditProductCommandHandler : IRequestHandler<EditProductCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        private readonly ICatalogDbContext _context;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<EditProductCommandHandler> _localizer;

        public EditProductCommandHandler(ICatalogDbContext context, IMapper mapper, IUploadService uploadService, IStringLocalizer<EditProductCommandHandler> localizer, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            if (await _context.Products.Where(p => p.Id != command.Id).AnyAsync(p => p.BarcodeSymbology == command.BarcodeSymbology, cancellationToken))
            {
                throw new CatalogException(_localizer["Barcode already exists."]);
            }

            var product = await _context.Products.Where(p => p.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if (product != null)
            {
                product = _mapper.Map<Product>(command);
                var uploadRequest = command.UploadRequest;
                if (uploadRequest != null)
                {
                    uploadRequest.FileName = $"P-{command.BarcodeSymbology}{uploadRequest.Extension}";
                    product.ImageUrl = _uploadService.UploadAsync(uploadRequest);
                }
                _context.Products.Update(product);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CatalogCacheKeys.GetProductByIdCacheKey(command.Id));
                return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Updated"]);
            }
            else
            {
                throw new CatalogException(_localizer["Product Not Found!"]);
            }
        }
    }
}
