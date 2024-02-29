using EtaLearning.API.Data;
using Microsoft.EntityFrameworkCore;
using EtaLearning.DataAccess.Data.Entities;

namespace EtaLearning.Core.Services
{
    public class SmartDeviceUpdater : ISmartDeviceUpdater
    {
        private readonly AppDbContext _dbContext;

        public SmartDeviceUpdater(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<SmartDevice> GetSmartDeviceByClientAsync(DbClient existingClient)
        {
            // Assuming existingClient.Id is an int
            var clientGuid = new Guid(existingClient.Id.ToString());

            var smartDevice = await _dbContext.SmartDevices
                .FirstOrDefaultAsync(sd => sd.Id == clientGuid);

            return smartDevice;
        }

        public async Task UpdateSmartDeviceAsync(DbClient existingSmartDevice)
        {
            SmartDevice smartDeviceToUpdate = await GetSmartDeviceByClientAsync(existingSmartDevice);

            if (smartDeviceToUpdate == null)
            {
                throw new InvalidOperationException("SmartDevice not found for update.");
            }

            smartDeviceToUpdate.Name = existingSmartDevice.Name;

            await UpdateSmartDeviceAsync(smartDeviceToUpdate);
        }

        public async Task UpdateSmartDeviceAsync(SmartDevice updatedSmartDevice)
        {
            SmartDevice existingSmartDevice = await _dbContext.SmartDevices.FindAsync(updatedSmartDevice.Id);

            if (existingSmartDevice == null)
            {
                throw new InvalidOperationException("SmartDevice not found for update.");
            }

            // Check if the timestamp matches
            if (existingSmartDevice.LastModified != updatedSmartDevice.LastModified)
            {
                // Timestamps don't match, indicate conflict
                throw new InvalidOperationException("Concurrency conflict: SmartDevice has been modified by another user.");
            }

            // Update the smart device properties
            existingSmartDevice.Name = updatedSmartDevice.Name;
            existingSmartDevice.LastModified = DateTime.UtcNow; // Update timestamp

            await _dbContext.SaveChangesAsync();
        }
    }
}
