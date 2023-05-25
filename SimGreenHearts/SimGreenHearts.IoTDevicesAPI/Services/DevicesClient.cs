using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using SimGreenHearts.IoTDevicesAPI.Interfaces;
using SimGreenHearts.Shared.DTO;
using System.Text;

namespace SimGreenHearts.IoTDevicesAPI.Services
{

    public class DevicesClient : IAzureIotHubClient
    {
        private readonly RegistryManager _registryManager;

        public DevicesClient(RegistryManager registryManager) => _registryManager = registryManager;

        public async Task<List<Device?>> GetDevicesFromHub(string hubName)
        {
            try
            {
                var query = _registryManager.CreateQuery("select * from devices");
                var result = await query.GetNextAsJsonAsync();

                return result.Select(d => JsonConvert.DeserializeObject<Device>(d)).ToList();
            }
            catch (Exception)
            {
                return new List<Device?>();
            }
        }


        public async Task<Device?> GetByDeviceId(string hubName, string deviceId)
        {
            return await _registryManager.GetDeviceAsync(deviceId);
        }

        public async Task<Device?> CreateDevice(string hubName, CreateDeviceDTO device)
        {
            var d = new Device(device.Id);
            d.Status = (DeviceStatus)device.Status;

            //TODO: modificar lo siguiente si vamos a aplicar conexion con certificados firmados x509

            if (!string.IsNullOrEmpty(device.PrimaryKey) && !string.IsNullOrEmpty(device.SecondaryKey))
            {
                d.Authentication = new AuthenticationMechanism();
                d.Authentication.SymmetricKey.PrimaryKey = System.Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(device.PrimaryKey));
                d.Authentication.SymmetricKey.SecondaryKey = System.Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(device.SecondaryKey));
            }

            return await _registryManager.AddDeviceAsync(d);
        }

        public async Task<Device?> ChangeDeviceStatus(string hubName, string deviceId)
        {
            var d = await _registryManager.GetDeviceAsync(deviceId);
            d.Status = d.Status == DeviceStatus.Enabled ? DeviceStatus.Disabled : DeviceStatus.Enabled;

            return await _registryManager.UpdateDeviceAsync(d);
        }

        public async Task<Device?> ChangeDeviceKeys(string hubName, string deviceId, UpdateDeviceKeysDTO newKeys)
        {

            var d = await _registryManager.GetDeviceAsync(deviceId);

            if (!string.IsNullOrEmpty(newKeys.PrimaryKey) && !string.IsNullOrEmpty(newKeys.SecondaryKey))
            {
                d.Authentication = new AuthenticationMechanism();
                d.Authentication.SymmetricKey.PrimaryKey = System.Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(newKeys.PrimaryKey));
                d.Authentication.SymmetricKey.SecondaryKey = System.Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(newKeys.SecondaryKey));
            }

            return await _registryManager.UpdateDeviceAsync(d);
        }

        public async Task<bool> DeleteDevice(string hubName, string deviceId)
        {
            try
            {
                await _registryManager.RemoveDeviceAsync(deviceId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
