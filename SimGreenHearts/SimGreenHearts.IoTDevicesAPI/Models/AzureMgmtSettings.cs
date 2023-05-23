namespace SimGreenHearts.IoTDevicesAPI.Models
{
    public class AzureMgmtSettings
    {
        public const string AzureMgmt = "AzureMgmt";
        public string? TenantId { get; set; }
        public string? Subscription { get; set; }
        public string? GraphEndpoint { get; set; }
        public string? ManagementEndpoint { get; set; }
        public string? ResourceGroupName { get; set; }
        public string? IoTHubName { get; set; }
        public string? RegistryName { get; set; }
    }
}
