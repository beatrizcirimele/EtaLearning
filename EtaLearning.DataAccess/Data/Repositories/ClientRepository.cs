using Microsoft.EntityFrameworkCore;

namespace EtaLearning.API.Data
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _dbContext;

        public ClientRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<DataAccess.Data.Entities.DbClient>> GetAllClientsAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<DataAccess.Data.Entities.DbClient> GetByIdAsync(int id)
        {
            return await _dbContext.Clients.FindAsync(id);
        }

        public async Task UpdateAsync(DataAccess.Data.Entities.DbClient client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                _dbContext.Clients.Remove(client);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DataAccess.Data.Entities.SmartDevice>> GetAllSmartDevicesAsync(Guid Id)
        {
            return await _dbContext.SmartDevices
                .Where(s => s.Id == Id)
                .ToListAsync();
        }

        public async Task<bool> IsSmartDeviceExistsAsync(Guid id)
        {
            return await _dbContext.SmartDevices.AnyAsync(s => s.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DataAccess.Data.Entities.DbClient>> GetAllAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<bool> IsClientExistsAsync(int id)
        {
            return await _dbContext.Clients.AnyAsync(c => c.Id == id);
        }

        public async Task CreateAsync(DataAccess.Data.Entities.DbClient entity)
        {
             _dbContext.Clients.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DataAccess.Data.Entities.DbClient> GetByNameAsync(string name)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Name == name);
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
                throw new Exception("SmartDevice not found."); // or any custom exception you prefer
            }
        }

    }
}