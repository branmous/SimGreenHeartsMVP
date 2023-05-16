using System.ComponentModel.DataAnnotations;

namespace SimGreenHearts.Shared.Entities
{
    public class EventType
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;
    }
}
