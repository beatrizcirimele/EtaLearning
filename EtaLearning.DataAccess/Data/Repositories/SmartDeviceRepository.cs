using EtaLearning.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using EtaLearning.DataAccess.Data.Interfaces;
using EtaLearning.API.Data;

namespace EtaLearning.DataAccess.Data.Repositories
{
    public class SmartDeviceRepository : ISmartDeviceRepository
    {
        private readonly AppDbContext _dbContext;

        public SmartDeviceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SmartDevice>> GetAllAsync()
        {
            return await _dbContext.SmartDevices.ToListAsync();
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

        public async Task DeleteAsync(Guid id)
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
    }
}
