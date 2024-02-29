using Microsoft.EntityFrameworkCore;
using EtaLearning.API.Data;
using EtaLearning.DataAccess.Data.Repositories;
using EtaLearning.DataAccess;
using Microsoft.OpenApi.Models;
using EtaLearning.Core.Services;

using Serilog;
using EtaLearning.API;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Register services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEtaLearningService, EtaLearningService>();
builder.Services.AddScoped<ISmartDeviceRepository, SmartDeviceRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISmartDeviceUpdater, SmartDeviceUpdater>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eta Learning", Version = "v1" });

    // Define the request body schema for the PostClient action
    c.SchemaGeneratorOptions.SchemaIdSelector = type => type.FullName;
      c.SchemaFilter<CustomSchemaFilter>();
});

// Enable logging using Serilog
Serilog.Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log.txt")
    .CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Call the seeding method
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    Seeder.AddNewData(services);
}

app.Run();
