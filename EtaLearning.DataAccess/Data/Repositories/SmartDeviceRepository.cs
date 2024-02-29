using EtaLearning.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using EtaLearning.API.Data;

namespace EtaLearning.DataAccess.Data.Repositories
{
    public class SmartDeviceRepository(AppDbContext dbContext) : ISmartDeviceRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<DbClient>> GetAllClientsAsync()
        {
            
            return await _dbContext.SmartDevices
                .Select(s => s.Client)
                .Distinct()
                .ToListAsync();
        }
        public async Task<IEnumerable<SmartDevice>> GetAllByClientIdAsync(Guid Id)
        {
            
            return await _dbContext.SmartDevices
                .Where(sd => sd.Id == Id)
                .ToListAsync();
        }

        public async Task<SmartDevice> GetByIdAsync(Guid id)
        {
            return await _dbContext.SmartDevices.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(SmartDevice smartDevice)
        {
            await _dbContext.SmartDevices.AddAsync(smartDevice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var smartDevice = await _dbContext.SmartDevices.FindAsync(id);
            if (smartDevice != null)
            {
                _dbContext.SmartDevices.Remove(smartDevice);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(SmartDevice smartDevice)
        {
            _dbContext.Entry(smartDevice).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsSmartDeviceExistsAsync(Guid id)
        {
            return await _dbContext.SmartDevices.AnyAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SmartDevice>> GetAllSmartDevicesAsync(Guid Id)
        {
            return await _dbContext.SmartDevices
                .Where(s => s.Id == Id)
                .ToListAsync();
        }

        public async Task CreateAsync(SmartDevice smartDevice)
        {
            _dbContext.SmartDevices.Add(smartDevice);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<SmartDevice>> GetAllAsync()
        {
            return await _dbContext.SmartDevices.ToListAsync();
        }

        public async Task UpdateSmartDeviceAsync(SmartDevice existingSmartDevice)
        {
            _dbContext.SmartDevices.Update(existingSmartDevice);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<SmartDevice> GetByIdAsync(int id)
        {
            return await _dbContext.SmartDevices.FindAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var smartDevice = await _dbContext.SmartDevices.FindAsync(id);

            if (smartDevice != null)
            {
                _dbContext.SmartDevices.Remove(smartDevice);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("SmartDevice not found.");
            }
        }
    }
}