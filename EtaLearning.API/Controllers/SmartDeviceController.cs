using Microsoft.AspNetCore.Mvc;
using EtaLearning.DataAccess.Data.Entities;
using EtaLearning.Core.Services;

namespace EtaLearning.API.Controllers
{
    [ApiController]
    [Route("api/smart-device")]
    public class SmartDeviceController : ControllerBase
    {
        private readonly IEtaLearningService _etaLearningService;
        private object _dbContext;
        private readonly ISmartDeviceRepository _smartDeviceRepository;

        public SmartDeviceController(IEtaLearningService etaLearningService, ISmartDeviceRepository smartDeviceRepository)
        {
            _etaLearningService = etaLearningService;
            _smartDeviceRepository = smartDeviceRepository ?? throw new ArgumentNullException(nameof(smartDeviceRepository));
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<SmartDevice>> GetAllSmartDevicesAsync() => 
            await _etaLearningService.GetAllAsync() as IEnumerable<SmartDevice>;

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSmartDeviceById(int id)
        {
            var smartDeviceId = id;

            var smartDevice = await _etaLearningService.GetByIdAsync(smartDeviceId);

            if (smartDevice == null)
            {
                return NotFound($"SmartDevice with Id {smartDeviceId} not found.");
            }

            return Ok(smartDevice);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditSmartDevice(int id, [FromBody] string name)
        {
            var existingSmartDevice = await _etaLearningService.GetSmartDeviceByIdAsync(id);

            if (existingSmartDevice == null)
            {
                return NotFound($"SmartDevice with Id {id} not found.");
            }

            if (existingSmartDevice.Name != name)
            {
                return Conflict($"The SmartDevice's name has been updated by another user. Refresh your data and try again.");
            }

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be empty.");
            }

            existingSmartDevice.Name = name;
            await _etaLearningService.UpdateSmartDeviceAsync( existingSmartDevice);

            return Ok(existingSmartDevice);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSmartDevice([FromBody] SmartDevice smartDevice)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _etaLearningService.CreateSmartDeviceAsync(smartDevice);

                return CreatedAtAction(nameof(GetSmartDeviceById), new { id = smartDevice.Id }, smartDevice);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Failed to create smart device");
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> CheckSmartDeviceExistence(Guid id)
        {
          
            bool exists = await _etaLearningService.IsSmartDeviceExistsAsync(id);
            _ = await _etaLearningService.IsSmartDeviceExistsAsync(id);

            return Ok(exists);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSmartDevice(int id)
        {
            var smartDeviceToDelete = await _etaLearningService.GetByIdAsync(id);

            if (smartDeviceToDelete == null)
            {
                return NotFound($"SmartDevice with Id {id} not found.");
            }

            await _etaLearningService.DeleteSmartDeviceAsync(id);
            return Ok();
        }
    }
    }
