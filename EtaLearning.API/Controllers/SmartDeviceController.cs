using EtaLearning.DataAccess.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EtaLearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmartDeviceController : ControllerBase
    {
        private readonly ISmartDeviceRepository _smartdeviceRepository;

        public SmartDeviceController(ISmartDeviceRepository smartdeviceRepository)
        {
            _smartdeviceRepository = smartdeviceRepository;
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetClientById(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return BadRequest("Invalid Id format. Please provide a valid GUID.");
            }

            var client = await _smartdeviceRepository.GetByIdAsync(guidId);

            if (client == null)
            {
                return NotFound($"Client with Id {guidId} not found.");
            }

            return Ok(client);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditClient(Guid id, [FromBody] string name)
        {
            var existingClient = await _smartdeviceRepository.GetByIdAsync(id);

            if (existingClient == null)
            {
                return NotFound($"Client with Id {id} not found.");
            }

         
            if (existingClient.Name != name)
            {
                return Conflict($"The client's name has been updated by another user. Refresh your data and try again.");
            }

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be empty.");
            }

            existingClient.Name = name;
            await _smartdeviceRepository.UpdateAsync(existingClient);
            return Ok(existingClient);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var clientToDelete = await _smartdeviceRepository.GetByIdAsync(id);

            if (clientToDelete == null)
            {
                return NotFound($"Client with Id {id} not found.");
            }

            await _smartdeviceRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
