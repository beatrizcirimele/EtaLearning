﻿using Microsoft.EntityFrameworkCore;
using EtaLearning.API.Data;
using EtaLearning.DataAccess.Data.Entities;
using Microsoft.Extensions.Logging;


namespace EtaLearning.Core.Service
{
    public class EtaLearningService : IEtaLearningService
    {
        private readonly ISmartDeviceUpdater _smartDeviceUpdater;
        private readonly IClientRepository _clientRepository;
        private readonly AppDbContext _dbContext;
        private readonly ISmartDeviceRepository _smartDeviceRepository;
        private readonly ILogger<EtaLearningService> _logger;

        public EtaLearningService(ISmartDeviceUpdater smartDeviceUpdater, IClientRepository clientRepository, AppDbContext dbContext, ISmartDeviceRepository smartDeviceRepository, ILogger<EtaLearningService> logger)
        {
            _logger = logger;
            _smartDeviceUpdater = smartDeviceUpdater ?? throw new ArgumentNullException(nameof(smartDeviceUpdater));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _smartDeviceRepository = smartDeviceRepository ?? throw new ArgumentNullException(nameof(smartDeviceRepository));
        }

        public async Task<IEnumerable<DbClient>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<DbClient> GetClientByIdAsync(int Id)
        {
            return await _clientRepository.GetByIdAsync(Id);
        }

        public async Task<DbClient> CreateClientAsync(DbClient client)
        {
            try
            {
                var existingClient = await _clientRepository.GetByNameAsync(client.Name);
                if (existingClient != null)
                {
                    throw new ArgumentException($"A client with the name '{client.Name}' already exists.");
                }

                await _clientRepository.CreateAsync(client);

                return client;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, $"An error occurred while creating client '{client.Name}'");

                // You can choose to rethrow the exception or handle it as needed
                throw;
            }
        }
        public async Task DeleteClientAsync(Guid Id)
        {
            await _clientRepository.DeleteAsync(Id);
        }

        public async Task UpdateAsync(DbClient client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        public async Task<bool> IsClientExistsAsync(int id)
        {
            return await _clientRepository.IsClientExistsAsync(id);
        }

        public async Task UpdateClientAsync(DbClient existingClient)
        {
            await _clientRepository.UpdateAsync(existingClient);
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _clientRepository.DeleteAsync(Id);
        }

        public async Task<bool> IsSmartDeviceExistsAsync(Guid id)
        {
            return await _dbContext.Set<SmartDevice>().AnyAsync(sd => sd.Id == id);
        }

        public async Task UpdateSmartDeviceAsync(DbClient existingClient, SmartDevice existingDevice, SmartDevice updatedDevice)
        {
            var existingSmartDevice = existingClient.SmartDevices.FirstOrDefault(sd => sd.Id == existingDevice.Id);

            if (existingSmartDevice == null)
            {

                throw new InvalidOperationException("Smart device not found in client's collection.");
            }
            existingSmartDevice.Name = updatedDevice.Name;
            existingSmartDevice.Kind = updatedDevice.Kind;
            existingSmartDevice.Type = updatedDevice.Type;
            existingSmartDevice.Created = updatedDevice.Created;

            await _smartDeviceRepository.UpdateAsync(existingSmartDevice);
        }

        public async Task<IEnumerable<SmartDevice>> GetAllAsync()
        {
            return await _dbContext.SmartDevices.ToListAsync();
        }

        public async Task<DbClient> GetByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public Task UpdateSmartDeviceAsync(DataAccess.Data.Entities.DbClient existingSmartDevice)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);

            if (client != null)
            {
                _dbContext.Clients.Remove(client);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<SmartDevice> GetByIdAsync(Guid id)
        {
            return await _dbContext.SmartDevices.FindAsync(id);
        }

        public async Task CreateSmartDeviceAsync(SmartDevice smartDevice)
        {
         
            await _smartDeviceRepository.AddAsync(smartDevice);
        }
    }
}
