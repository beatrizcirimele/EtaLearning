using EtaLearning.DataAccess.Data.Entities;

public interface ISmartDeviceUpdater
{
    Task<SmartDevice> GetSmartDeviceByClientAsync(DbClient existingSmartDevice);
    Task UpdateSmartDeviceAsync(SmartDevice smartDevice);
}