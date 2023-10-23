using EtaLearning.API.Data.Entities;


namespace EtaLearning.API.Data
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task CreateAsync(Client client);
        Task<bool> IsClientExistsAsync(int id);
        Task AddAsync(Client client);
    }
}
