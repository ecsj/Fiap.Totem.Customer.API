using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Domain.Request;

namespace API.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IClientUseCase _clientUseCase;

    public UserController(IClientUseCase client)
    {
        _clientUseCase = client;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var orders = _clientUseCase.Get();

        return Ok(orders);
    }

    [HttpGet("{cpf}")]
    public async Task<IActionResult> GetByCpf(string cpf)
    {
        var order = await _clientUseCase.GetByCpf(cpf);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ClientRequest request)
    {
        var client = await _clientUseCase.Add(request);

        return CreatedAtAction(nameof(GetByCpf), new { cpf = client.CPF }, client);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] ClientRequest request)
    {
        await _clientUseCase.Update(id, request);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _clientUseCase.Delete(id);

        return NoContent();
    }
}