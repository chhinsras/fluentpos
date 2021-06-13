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

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Commands
{
    public partial class EditCategoryCommand : IRequest<Result<Guid>>
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

    internal class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<EditCategoryCommandHandler> _localizer;

        public EditCategoryCommandHandler(ICatalogDbContext context, IMapper mapper, IUploadService uploadService, IStringLocalizer<EditCategoryCommandHandler> localizer, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(EditCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (category != null)
            {
                category = _mapper.Map<Category>(command);
                var uploadRequest = command.UploadRequest;
                if (uploadRequest != null)
                {
                    uploadRequest.FileName = $"C-{command.Name}{uploadRequest.Extension}";
                    category.ImageUrl = _uploadService.UploadAsync(uploadRequest);
                }
                _context.Categories.Update(category);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CatalogCacheKeys.GetCategoryByIdCacheKey(command.Id));
                return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Updated"]);
            }
            else
            {
                throw new CatalogException(_localizer["Category Not Found!"]);
            }
        }
    }
}