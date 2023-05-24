using System.Text.Json.Serialization;

namespace SimGreenHearts.Shared.DTO
{
    public class DeviceListDTO
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("connectionState")]
        public int ConnectionState { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("connectionStateUpdatedTime")]
        public DateTime? ConnectionStateUpdatedTime { get; set; }

        [JsonPropertyName("statusUpdatedTime")]
        public DateTime? StatusUpdatedTime { get; set; }

        [JsonPropertyName("lastActivityTime")]
        public DateTime? LastActivityTime { get; set; }

        [JsonPropertyName("authentication")]
        public Authentication? Authentication { get; set; }
    }

    public class Authentication
    {
        [JsonPropertyName("symmetricKey")]
        public SymmetricKey? SymmetricKey { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }
    }

    public class SymmetricKey
    {
        [JsonPropertyName("primaryKey")]
        public string? PrimaryKey { get; set; }

        [JsonPropertyName("secondaryKey")]
        public string? SecondaryKey { get; set; }
    }
}
