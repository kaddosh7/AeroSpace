using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeroSpace.Models
{
    public partial class Modelo
    {
        public int IdModelo { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public double? Propulsion { get; set; }

        public int? Motores { get; set; }
        public double? Peso { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int? AvionId { get; set; }

        public byte EstadoModelo { get; set; }

        public virtual Avion? Avion { get; set; }
    }
}
