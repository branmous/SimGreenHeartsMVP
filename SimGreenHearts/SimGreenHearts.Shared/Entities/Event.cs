using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimGreenHearts.Shared.Entities
{
    public class Event
    {
        public int Id { get; set; }


        [Display(Name = "Categoría")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Observation { get; set; }

        public DateTime CreatedDate { get; set; }

        public EventType? EventType { get; set; }

        public int EventTypeId { get; set; }

    }
}
