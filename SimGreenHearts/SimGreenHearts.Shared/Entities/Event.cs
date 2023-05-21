using System.ComponentModel.DataAnnotations;

namespace SimGreenHearts.Shared.Entities
{
    public class Event
    {
        public int Id { get; set; }


        [Display(Name = "Categoría")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Observation { get; set; }

        [Display(Name = "Fecha de creación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime CreatedDate { get; set; }

        public EventType? EventType { get; set; }

        [Display(Name = "Evento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int EventTypeId { get; set; }

        public User? User { get; set; }
        public string UserId { get; set; } = null!;

    }
}
