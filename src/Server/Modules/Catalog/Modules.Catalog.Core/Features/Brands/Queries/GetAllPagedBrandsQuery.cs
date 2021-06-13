using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using FluentPOS.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetAllPagedBrandsQuery : IRequest<PaginatedResult<GetAllPagedBrandsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
  
        public GetAllPagedBrandsQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }

    internal class GetAllPagedBrandsCachedQueryHandler : IRequestHandler<GetAllPagedBrandsQuery, PaginatedResult<GetAllPagedBrandsResponse>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllPagedBrandsCachedQueryHandler> _localizer;

        public GetAllPagedBrandsCachedQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<GetAllPagedBrandsCachedQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPagedBrandsResponse>> Handle(GetAllPagedBrandsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Brand, GetAllPagedBrandsResponse>> expression = e => new GetAllPagedBrandsResponse(e.Id, e.Name, e.Detail);

            var queryable = _context.Brands.OrderBy(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchString)) queryable = queryable.Where(b => b.Name.Contains(request.SearchString) || b.Detail.Contains(request.SearchString));
           
            var brandList = await queryable
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);


            if (brandList == null) throw new CatalogException(_localizer["Brand Not Found!"]);
            // TODO: Cache
            var mappedBrands = _mapper.Map<PaginatedResult<GetAllPagedBrandsResponse>>(brandList);

            return mappedBrands;

        }
    }
}