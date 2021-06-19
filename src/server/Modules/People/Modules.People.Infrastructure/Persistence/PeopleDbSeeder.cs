using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Infrastructure.Persistence
{
    public class PeopleDbSeeder : IDatabaseSeeder
    {
        private readonly ILogger<PeopleDbSeeder> _logger;
        private readonly PeopleDbContext _db;

        public PeopleDbSeeder(ILogger<PeopleDbSeeder> logger, PeopleDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                AddCustomers();
                _db.SaveChanges();
            }
            catch (System.Exception)
            {
                _logger.LogError("An error occurred while seeding People data.");
            }
        }

        private void AddCustomers()
        {
            Task.Run(async () =>
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!_db.Customers.Any())
                {
                    var customerData = await File.ReadAllTextAsync(path + @"/Persistence/SeedData/customers.json");
                    var customers = JsonConvert.DeserializeObject<List<Customer>>(customerData);

                    if (customers != null)
                    {
                        foreach (var customer in customers)
                        {
                            _db.Customers.Add(customer);
                        }
                    }

                    await _db.SaveChangesAsync();
                    _logger.LogInformation("Seeded Customers.");
                }
            }).GetAwaiter().GetResult();
        }

    }
}
