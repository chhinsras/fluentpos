// --------------------------------------------------------------------------------------------------
// <copyright file="CatalogDbSeeder.cs" company="FluentPOS">
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
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Interfaces.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence
{
    public class CatalogDbSeeder : IDatabaseSeeder
    {
        private readonly ILogger<CatalogDbSeeder> _logger;
        private readonly CatalogDbContext _db;
        private readonly IStringLocalizer<CatalogDbSeeder> _localizer;
        private readonly IJsonSerializer _jsonSerializer;

        public CatalogDbSeeder(ILogger<CatalogDbSeeder> logger, CatalogDbContext db, IStringLocalizer<CatalogDbSeeder> localizer, IJsonSerializer jsonSerializer)
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
                AddBrands();
                AddCategories();
                AddProducts();
                _db.SaveChanges();
            }
            catch (Exception)
            {
                _logger.LogError(_localizer["An error occurred while seeding Catalog data."]);
            }
        }

        private void AddBrands()
        {
            Task.Run(async () =>
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!_db.Brands.Any())
                {
                    string brandData = await File.ReadAllTextAsync(path + @"/Persistence/SeedData/brands.json");
                    var brands = _jsonSerializer.Deserialize<List<Brand>>(brandData);

                    if (brands != null)
                    {
                        foreach (var brand in brands)
                        {
                            await _db.Brands.AddAsync(brand);
                        }
                    }

                    await _db.SaveChangesAsync();
                    _logger.LogInformation(_localizer["Seeded Brands."]);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddCategories()
        {
            Task.Run(async () =>
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!_db.Categories.Any())
                {
                    string categoryData = await File.ReadAllTextAsync(path + @"/Persistence/SeedData/categories.json");
                    var categories = _jsonSerializer.Deserialize<List<Category>>(categoryData);

                    if (categories != null)
                    {
                        foreach (var category in categories)
                        {
                            await _db.Categories.AddAsync(category);
                        }
                    }

                    await _db.SaveChangesAsync();
                    _logger.LogInformation(_localizer["Seeded Categories."]);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddProducts()
        {
            Task.Run(async () =>
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!_db.Products.Any())
                {
                    string productData = await File.ReadAllTextAsync(path + @"/Persistence/SeedData/products.json");
                    var products = _jsonSerializer.Deserialize<List<Product>>(productData);

                    if (products != null)
                    {
                        foreach (var product in products)
                        {
                            await _db.Products.AddAsync(product);
                        }
                    }

                    await _db.SaveChangesAsync();
                    _logger.LogInformation(_localizer["Seeded Products."]);
                }
            }).GetAwaiter().GetResult();
        }
    }
}