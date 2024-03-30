using Domain.Entities;
using Domain.Repositories.Base;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<Client> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Client>().Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<Client> Get()
    {
        var entitySet = _dbContext.Set<Client>().Include(x => x.Address);

        return entitySet;
    }

    public async Task<Client> GetByCpf(string cpf)
    {
        return await _dbContext.Set<Client>().Include(x => x.Address).FirstOrDefaultAsync(c => c.CPF == cpf);
    }
}