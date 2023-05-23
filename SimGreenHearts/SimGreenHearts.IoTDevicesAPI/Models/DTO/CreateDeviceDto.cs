namespace SimGreenHearts.IoTDevicesAPI.Models.DTO
{
    public class CreateDeviceDto
    {
        public string Id { get; set; } = string.Empty;
        public int Status { get; set; }
        public string? PrimaryKey { get; set; }
        public string? SecondaryKey { get; set; }
    }
}
