// --------------------------------------------------------------------------------------------------
// <copyright file="PeopleDbSeeder.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Interfaces.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Modules.People.Infrastructure.Persistence
{
    public class PeopleDbSeeder : IDatabaseSeeder
    {
        private readonly ILogger<PeopleDbSeeder> _logger;
        private readonly PeopleDbContext _db;
        private readonly IStringLocalizer<PeopleDbContext> _localizer;
        private readonly IJsonSerializer _jsonSerializer;

        public PeopleDbSeeder(
            ILogger<PeopleDbSeeder> logger,
            PeopleDbContext db,
            IStringLocalizer<PeopleDbContext> localizer,
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _db = db;
            _localizer = localizer;
            _jsonSerializer = jsonSerializer;
        }

        public void Initialize()
        {
            try
            {
                AddCustomers();
                _db.SaveChanges();
            }
            catch (Exception)
            {
                _logger.LogError(_localizer["An error occurred while seeding People data."]);
            }
        }

        private void AddCustomers()
        {
            Task.Run(async () =>
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!_db.Customers.Any())
                {
                    string customerData = await File.ReadAllTextAsync(path + @"/Persistence/SeedData/customers.json");
                    var customers = _jsonSerializer.Deserialize<List<Customer>>(customerData);

                    if (customers != null)
                    {
                        foreach (var customer in customers)
                        {
                            await _db.Customers.AddAsync(customer);
                        }
                    }

                    await _db.SaveChangesAsync();
                    _logger.LogInformation(_localizer["Seeded Customers."]);
                }
            }).GetAwaiter().GetResult();
        }
    }
}