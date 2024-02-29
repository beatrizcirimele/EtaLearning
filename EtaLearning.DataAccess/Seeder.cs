using EtaLearning.API.Data;
using EtaLearning.DataAccess.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EtaLearning.DataAccess
{
    public static class Seeder
    {
        public static void AddNewData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Check if Clients table is empty, then add new clients
                if (!dbContext.Clients.Any())
                {
                    dbContext.Clients.AddRange(
                        new DbClient { Name = "Lustitia Ltd", CreationDate = DateTime.UtcNow },
                        new DbClient { Name = "Bachmann", CreationDate = DateTime.UtcNow }
                    );
                }

                // Check if SmartDevices table is empty, then add new smart devices
                if (!dbContext.SmartDevices.Any())
                {
                    var firstClient = dbContext.Clients.FirstOrDefault();

                    if (firstClient != null)
                    {
                        dbContext.SmartDevices.AddRange(
                            new SmartDevice { Name = "WMZ 00006696", Created = DateTime.UtcNow, Kind = 2, Type = 201, ClientId = firstClient.Id },
                            new SmartDevice { Name = "WMZ Berlin", Created = DateTime.UtcNow, Kind = 2, Type = 202, ClientId = firstClient.Id },
                            new SmartDevice { Name = "WMZ Hamburg", Created = DateTime.UtcNow, Kind = 2, Type = 203, ClientId = firstClient.Id }
                        );

                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
