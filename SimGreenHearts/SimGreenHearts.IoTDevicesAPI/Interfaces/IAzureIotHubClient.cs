using Microsoft.Azure.Devices;
using SimGreenHearts.IoTDevicesAPI.Models.DTO;

namespace SimGreenHearts.IoTDevicesAPI.Interfaces
{
    public interface IAzureIotHubClient
    {
        Task<Device?> ChangeDeviceKeys(string hubName, string deviceId, UpdateDeviceKeysDto newKeys);
        Task<Device?> ChangeDeviceStatus(string hubName, string deviceId);
        Task<Device?> CreateDevice(string hubName, CreateDeviceDto device);
        Task<bool> DeleteDevice(string hubName, string deviceId);
        Task<Device?> GetByDeviceId(string hubName, string deviceId);
        Task<List<Device?>> GetDevicesFromHub(string hubName);
    }
}