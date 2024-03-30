using Application.Interfaces;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories.Base;
using Domain.Request;

namespace Application.UseCases;

public class ClientUseCase : IClientUseCase
{
    private readonly IClientRepository _clienteRepository;
    private readonly IMessageQueueService _queueService;

    public ClientUseCase(IClientRepository clienteRepository, IMessageQueueService queueService)
    {
        _clienteRepository = clienteRepository;
        _queueService = queueService;
    }

    public IQueryable<Client> Get()
    {
        return _clienteRepository.Get();
    }

    public async Task<Client> GetById(Guid id)
    {
        return await _clienteRepository.GetByIdAsync(id);
    }

    public async Task<Client> GetByCpf(string cpf)
    {
        return await _clienteRepository.GetByCpf(cpf);
    }
    public async Task<Client> Add(ClientRequest request)
    {
        var client = Client.FromRequest(request);

        var existClient = await GetByCpf(client.CPF);

        if (existClient != null) throw new Exception("CPF já cadastrado");

        await _clienteRepository.AddAsync(client);

        _queueService.PublishMessage("Totem.Customer.Created", client.ToJson());

        return client;
    }

    public async Task Update(Guid id, ClientRequest clientRequest)
    {
        var client = await GetById(id);

        if (client is null) throw new DomainException("Client não encontrado");

        _queueService.PublishMessage("Totem.Customer.Updated", client.ToJson());


        await _clienteRepository.UpdateAsync(client);
    }

    public async Task Delete(Guid id)
    {
        var client = await GetById(id);

        if (client is null) throw new DomainException("Client não encontrado");

        _queueService.PublishMessage("Totem.Customer.Deleted", client.ToJson());


        await _clienteRepository.DeleteAsync(client);
    }
}