using EtaLearning.DataAccess.Data.Entities;

public interface IClientRepository : IRepository<DbClient>
{
    Task CreateAsync(DbClient entity);
    new Task<IEnumerable<DbClient>> GetAllAsync();
    Task<IEnumerable<DbClient>> GetAllClientsAsync();
    Task<DbClient> GetByNameAsync(string name);
    Task<bool> IsClientExistsAsync(int id);
    Task UpdateAsync(DbClient client);
}

public interface ISmartDeviceRepository : IRepository<SmartDevice>
{
    Task<IEnumerable<SmartDevice>> GetAllByClientIdAsync(Guid Id);
    Task<bool> IsSmartDeviceExistsAsync( Guid id);
    Task<SmartDevice> GetByIdAsync(Guid id);
    Task AddAsync(SmartDevice smartDevice);
}

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<int> SaveChangesAsync();
}
