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

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands
{
    public partial class AddBrandCommand : IRequest<Result<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Detail { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }
}