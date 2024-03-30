using Domain.Entities;

namespace Domain.Repositories.Base;

public interface IClientRepository : IRepository<Client>
{
    Task<Client> GetByIdAsync(Guid id);
    Task<Client> GetByCpf(string cpf);

}