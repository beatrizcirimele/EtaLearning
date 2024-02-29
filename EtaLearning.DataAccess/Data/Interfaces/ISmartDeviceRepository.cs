using EtaLearning.DataAccess.Data.Entities;


namespace EtaLearning.DataAccess.Data.Interfaces
{
    public interface ISmartDeviceRepository
    {
        Task<List<SmartDevice>> GetAllAsync();
        Task<SmartDevice> GetByIdAsync(Guid id);
        Task AddAsync(SmartDevice smartDevice);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(SmartDevice smartDevice);
        Task<bool> IsSmartDeviceExistsAsync(Guid id);
        Task<IEnumerable<SmartDevice>> GetAllSmartDevicesAsync();
        Task UpdateSmartDeviceAsync(SmartDevice smartDevice);
      
        Task CreateSmartDeviceAsync(SmartDevice smartDevice);
        Task DeleteSmartDeviceAsync(Guid smartDeviceId);
        Task AddSmartDeviceAsync(SmartDevice smartDevice);
        
}
}
