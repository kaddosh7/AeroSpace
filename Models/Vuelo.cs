using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroSpace.Models
{
    public partial class Vuelo
    {
        public int IdVuelo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? Fecha { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]

        [NotMapped]
        public TimeOnly Hora { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string TipoVuelo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? AvionId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? PilotoId { get; set; }
        public int? Copiloto { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string TipoAvion { get; set; }
        public byte EstadoVuelo { get; set; }

        public virtual Avion Avion { get; set; }
        public virtual Piloto Piloto { get; set; }
        //public virtual Persona? Persona { get; set; }
    }
}
