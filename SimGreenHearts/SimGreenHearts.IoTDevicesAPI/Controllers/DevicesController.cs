using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using SimGreenHearts.IoTDevicesAPI.Interfaces;
using SimGreenHearts.IoTDevicesAPI.Models.DTO;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SimGreenHearts.IoTDevicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class DevicesController : ControllerBase
    {
        private readonly IAzureIotHubClient _devicesClient;
        public DevicesController(IAzureIotHubClient devicesClient) => _devicesClient = devicesClient;

        [HttpGet("{HubName}")]
        public async Task<IActionResult> GetDevicesAsync(string HubName = "iothub-xm-dev")
        {
            return Ok(await _devicesClient.GetDevicesFromHub(HubName));
        }

        [HttpGet("{HubName}/{DeviceId}")]
        public async Task<IActionResult> GetDeviceAsync(string DeviceId, string HubName = "iothub-xm-dev")
        {
            var device = await _devicesClient.GetByDeviceId(HubName, DeviceId);
            if (device == null) return NotFound();
            return Ok(device);
        }


        [HttpPost("{HubName}")]
        public async Task<IActionResult> Create(string HubName, [FromBody] CreateDeviceDto device)
        {
            try
            {
                var newDevice = await _devicesClient.CreateDevice(HubName, device);
                return Ok(newDevice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPut("{HubName}/ChangeStatus/{DeviceId}")]
        public async Task<IActionResult> ChangeStatus(string HubName, string DeviceId)
        {
            try
            {
                var resultDevice = await _devicesClient.ChangeDeviceStatus(HubName, DeviceId);
                return Ok(resultDevice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{HubName}/ChangeKeys/{DeviceId}")]
        public async Task<IActionResult> ChangeKeys(string HubName, string DeviceId, [FromBody] UpdateDeviceKeysDto newKeys)
        {
            try
            {
                var resultDevice = await _devicesClient.ChangeDeviceKeys(HubName, DeviceId, newKeys);
                return Ok(resultDevice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpDelete("{HubName}/{DeviceId}")]
        public async Task<IActionResult> Delete(string HubName, string DeviceId)
        {
            var result = await _devicesClient.DeleteDevice(HubName, DeviceId);
            if (!result) return Content("Device could be not");
            return Ok($"device {DeviceId} deleted");
        }

    }
}
