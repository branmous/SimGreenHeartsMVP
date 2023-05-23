using System.ComponentModel.DataAnnotations;

namespace SimGreenHearts.Shared.DTO
{
    public class EventDTO
    {
        [Display(Name = "Observación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Observation { get; set; } = null!;

        [Display(Name = "Tipo de Evento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        public int EventId { get; set; }
    }
}
