using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Constants;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Modules.People.Core.Features.Customers.Events;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Customers.Commands
{
    internal class CustomerCommandHandler :
        IRequestHandler<RegisterCustomerCommand, Result<Guid>>,
        IRequestHandler<RemoveCustomerCommand, Result<Guid>>,
        IRequestHandler<UpdateCustomerCommand, Result<Guid>>

    {
        private readonly IDistributedCache _cache;
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<CustomerCommandHandler> _localizer;

        public CustomerCommandHandler(IPeopleDbContext context, IMapper mapper, IUploadService uploadService, IStringLocalizer<CustomerCommandHandler> localizer, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(command);
            if (customer.Type != CustomerTypes.General || customer.Type != CustomerTypes.VIP) customer.Type = CustomerTypes.General;
            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"C-{command.Name}{uploadRequest.Extension}";
                customer.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
            }
            customer.AddDomainEvent(new CustomerRegisteredEvent(customer));
            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(customer.Id, _localizer["Customer Saved"]);
        }

        public async Task<Result<Guid>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (customer != null)
            {
                customer = _mapper.Map<Customer>(command);
                if (customer.Type != CustomerTypes.General && customer.Type != CustomerTypes.VIP) customer.Type = CustomerTypes.General;
                var uploadRequest = command.UploadRequest;
                if (uploadRequest != null)
                {
                    uploadRequest.FileName = $"C-{command.Name}{uploadRequest.Extension}";
                    customer.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
                }
                customer.AddDomainEvent(new CustomerUpdatedEvent(customer));
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(PeopleCacheKeys.GetCustomerByIdCacheKey(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(customer.Id, _localizer["Customer Updated"]);
            }
            else
            {
                throw new PeopleException(_localizer["Customer Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(RemoveCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
            customer.AddDomainEvent(new CustomerRemovedEvent(customer.Id));
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(PeopleCacheKeys.GetCustomerByIdCacheKey(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(customer.Id, _localizer["Customer Deleted"]);
        }
    }
}