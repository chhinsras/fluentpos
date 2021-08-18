// --------------------------------------------------------------------------------------------------
// <copyright file="EntityReferenceService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.Interfaces;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class EntityReferenceService : IEntityReferenceService
    {
        private readonly IApplicationDbContext _context;

        public EntityReferenceService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> TrackAsync(string entityName)
        {
            string referenceNumber;
            string monthYearString = DateTime.Now.ToString("MMyy");
            var record = _context.EntityReferences.FirstOrDefault(a => a.Entity == entityName && a.MonthYearString == monthYearString);
            if (record != null)
            {
                record.Increment();
                _context.EntityReferences.Update(record);
                referenceNumber = GenerateReferenceNumber(entityName, record.Count, monthYearString);
            }
            else
            {
                record = new EntityReference(entityName);
                _context.EntityReferences.Add(record);
                referenceNumber = GenerateReferenceNumber(entityName, record.Count, monthYearString);
            }

            await _context.SaveChangesAsync();
            return referenceNumber;
        }

        private string GenerateReferenceNumber(string entity, int count, string monthYearString)
        {
            return $"{entity[0]}-{monthYearString}-{count.ToString().PadLeft(5, '0')}";
        }
    }
}