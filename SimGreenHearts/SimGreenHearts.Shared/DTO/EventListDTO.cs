using SimGreenHearts.Shared.Entities;

namespace SimGreenHearts.Shared.DTO
{
    public class EventListDTO
    {
        public int Id { get; set; }
        public string? Observation { get; set; }

        public DateTime CreatedDate { get; set; }
        public EventType? EventType { get; set; }

        public string? UserFullName { get; set; }
    }
}
