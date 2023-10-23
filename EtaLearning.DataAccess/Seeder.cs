using EtaLearning.API.Data.Entities;
using EtaLearning.API.Data;
using Microsoft.Extensions.DependencyInjection;
using EtaLearning.DataAccess.Data.Entities;

namespace EtaLearning.DataAccess
{
    public static class Seeder
    {
        public static void AddNewData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!dbContext.Clients.Any())
                {
                    dbContext.Clients.AddRange(
                           new Client { Name = "Lustitia Ltd", CreationDate = DateTime.UtcNow },
                           new Client { Name = "Bachmann", CreationDate = DateTime.UtcNow }
                       );
                    dbContext.SaveChanges();
                }

                if (!dbContext.SmartDevices.Any())
                {
                    dbContext.SmartDevices.AddRange(
                           new SmartDevice { Name = "WMZ 00006696", Created = DateTime.UtcNow, Id = 
                           new Guid("00A1B1E8-C4A4-4ADE-BCC9-0B95CFEFD209"), Kind = 2,
                           Type = 201,
                           }
                          
                       );
                    dbContext.SaveChanges();
                }

            }
        }
    }
}
