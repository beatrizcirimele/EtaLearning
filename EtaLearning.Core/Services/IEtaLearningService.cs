using EtaLearning.DataAccess.Data.Entities;

namespace EtaLearning.Core.Services
{
    public interface IEtaLearningService
    {
        // Client Methods
        Task<IEnumerable<DbClient>> GetAllClientsAsync();
        Task<DbClient> GetByIdAsync(int id);
        Task<DbClient> CreateClientAsync(DbClient client);
        Task UpdateClientAsync(DbClient existingClient); 
        Task DeleteClientAsync(int id);
        Task<bool> IsClientExistsAsync(int id);

        // Smart Device Methods
        Task<bool> IsSmartDeviceExistsAsync(Guid id);
        Task<bool> DeleteSmartDeviceAsync(int id);

        Task UpdateAsync(DbClient existingClient);
        Task<IEnumerable<SmartDevice>> GetAllAsync();
        Task UpdateSmartDeviceAsync(DbClient existingClient,SmartDevice existingSmartDevice, SmartDevice updatedSmartDevice);
        Task UpdateSmartDeviceAsync(DbClient existingSmartDevice);
        Task<SmartDevice> GetSmartDeviceByIdAsync(int id);
        Task CreateSmartDeviceAsync(SmartDevice smartDevice);
        Task UpdateSmartDeviceAsync(SmartDevice existingSmartDevice);
    }
}