using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Application.Interfaces.Services;
using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands
{
    public partial class EditBrandCommand : IRequest<Result<Guid>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Detail { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class EditBrandCommandHandler : IRequestHandler<EditBrandCommand, Result<Guid>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<EditBrandCommandHandler> _localizer;

        public EditBrandCommandHandler(ICatalogDbContext context, IMapper mapper, IUploadService uploadService, IStringLocalizer<EditBrandCommandHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(EditBrandCommand command, CancellationToken cancellationToken)
        {
            var brand = await _context.Brands.Where(b => b.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (brand != null)
            {
                brand = _mapper.Map<Brand>(command);
                var uploadRequest = command.UploadRequest;
                if (uploadRequest != null)
                {
                    uploadRequest.FileName = $"B-{command.Name}{uploadRequest.Extension}";
                    brand.ImageUrl = _uploadService.UploadAsync(uploadRequest);
                }
                _context.Brands.Update(brand);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(brand.Id, _localizer["Brand Updated"]);
            }
            else
            {
                throw new CatalogException(_localizer["Brand Not Found!"]);
            }
        }
    }
}