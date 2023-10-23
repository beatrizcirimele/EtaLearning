using EtaLearning.DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EtaLearning.DataAccess.Data.Interfaces
{
    public interface ISmartDeviceRepository
    {
        Task<List<SmartDevice>> GetAllAsync();
        Task<SmartDevice> GetByIdAsync(Guid id);
        Task AddAsync(SmartDevice smartDevice);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(SmartDevice smartDevice);
    }
}
