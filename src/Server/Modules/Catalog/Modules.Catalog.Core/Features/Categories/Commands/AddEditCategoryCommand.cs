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
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Commands
{
    public partial class AddEditCategoryCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Detail { get; set; }

        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditCategoryCommandHandler : IRequestHandler<AddEditCategoryCommand, Result<Guid>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditCategoryCommandHandler> _localizer;

        public AddEditCategoryCommandHandler(ICatalogDbContext context, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditCategoryCommandHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(AddEditCategoryCommand command, CancellationToken cancellationToken)
        {
            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"C-{command.Name}{uploadRequest.Extension}";
            }

            if (command.Id == Guid.Empty)
            {
                var category = _mapper.Map<AddEditCategoryCommand, Category>(command);
                if (uploadRequest != null)
                {
                    category.ImageUrl = _uploadService.UploadAsync(uploadRequest);
                }
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Saved"]);
            }
            else
            {
                var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == command.Id);
                if (category != null)
                {
                    category.Name = command.Name ?? category.Name;
                    if (uploadRequest != null)
                    {
                        category.ImageUrl = _uploadService.UploadAsync(uploadRequest);
                    }
                    category.ImageUrl = command.ImageUrl ?? category.ImageUrl;
                    category.Detail = command.Detail ?? category.Detail;
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync(cancellationToken);
                    return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Updated"]);
                }
                else
                {
                    throw new CatalogException(_localizer["Category Not Found!"]);
                }
            }
        }
    }
}