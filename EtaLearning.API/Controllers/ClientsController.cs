using EtaLearning.Core.Services;
using Microsoft.AspNetCore.Mvc;


namespace EtaLearning.API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IEtaLearningService _etaLearningService;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IEtaLearningService etaLearningService, ILogger<ClientsController> logger)
        {
            _etaLearningService = etaLearningService;
            _logger = logger;
        }

        [HttpGet("get-clients")]
        public async Task<IActionResult> GetClientsAsync()
        {
            var clients = await _etaLearningService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _etaLearningService.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditClient(int id, [FromBody] DataAccess.Data.Entities.DbClient client)
        {
            if (id != client.Id)
            {
                ModelState.AddModelError("Id", "Client ID in URL does not match client ID in request body.");
            }

            if (client.Id == 0)
            {
                ModelState.AddModelError("Id", "Invalid client ID format.");
            }

            if (client.SmartDevices != null && client.SmartDevices.Any())
            {
                foreach (var smartDevice in client.SmartDevices)
                {
                    if (id != client.Id.GetHashCode())

                    {
                        ModelState.AddModelError("SmartDeviceId", "Invalid smart device ID format.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingClient = await _etaLearningService.GetByIdAsync(id);

            if (existingClient == null)
            {
                return NotFound();
            }

            existingClient.Name = client.Name;

            await _etaLearningService.UpdateAsync(existingClient);

            return Ok(existingClient);
        }

        [HttpPost("create")]
        public async Task<ActionResult<DataAccess.Data.Entities.DbClient>> PostClient([FromBody] DataAccess.Data.Entities.DbClient client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdClient = await _etaLearningService.CreateClientAsync(client);

                // Return the created client in the response body
                return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
            }
            catch (Exception ex)
            {
                // Log the exception and its inner exception details
                _logger.LogError(ex, "An error occurred while processing the request.");

                // Return a bad request response with the error message
                return BadRequest($"Failed to create client: {ex.Message}");
            }
        }

        [HttpGet("exist/{id}")]
        public async Task<IActionResult> CheckClientExistence(int id)
        {
            var exists = await _etaLearningService.IsClientExistsAsync(id);
            return Ok(exists);
        }
    }
}
