using System.ComponentModel.DataAnnotations;

namespace SimGreenHearts.Shared.DTO
{
    public class CreateDeviceDTO
    {
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Estado del dispositivo")]
        public int Status { get; set; }

        [Display(Name = "Clave primaria")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string? PrimaryKey { get; set; }


        [Display(Name = "Clave secundaria")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string? SecondaryKey { get; set; }


        [Display(Name = "Estado de conexión")]
        public int ConnexionStatus { get; set; }

        [Display(Name = "Clave secundaria")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string Latitude { get; set; } = string.Empty;

        [Display(Name = "Clave secundaria")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string Longitude { get; set; } = string.Empty;
    }
}
