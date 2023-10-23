using EtaLearning.API.Data;
using EtaLearning.DataAccess.Data.Interfaces;
using EtaLearning.DataAccess.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EtaLearning.DataAccess
{
    public static class DependencyInjection
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<ISmartDeviceRepository, SmartDeviceRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
