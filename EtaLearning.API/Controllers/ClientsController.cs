using EtaLearning.API.Data;
using EtaLearning.API.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EtaLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _clientRepository;

    public ClientsController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    [HttpGet()]
    public IActionResult GetClients()
    {
        var clients = _clientRepository.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditClient(int id, [FromBody] string name)
    {
        var existingClient = await _clientRepository.GetByIdAsync(id);

        if (existingClient == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(name))
        {
            existingClient.Name = name;
            await _clientRepository.UpdateAsync(existingClient);
        }

        return Ok(existingClient);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> CreateClient([FromBody] Client client)
    {
        if (client == null)
        {
            return BadRequest("Invalid client data. Please provide valid data.");
        }
        await _clientRepository.AddAsync(client);
        return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var clientToDelete = await _clientRepository.GetByIdAsync(id);

        if (clientToDelete == null)
        {
            return NotFound();
        }

        await _clientRepository.DeleteAsync(id);
        
        return Ok();
    }

    [HttpGet("{id}/exist")]
    public async Task<IActionResult> CheckClientExistence(int id)
    {
        var exists = await _clientRepository.IsClientExistsAsync(id);

        return Ok(exists);
    }
}
