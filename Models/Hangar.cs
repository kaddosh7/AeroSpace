using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeroSpace.Models
{
    public partial class Hangar
    {
        public int IdHangar { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Ubicacion { get; set; }

        [Required]
        [Display(Name = "Capacidad de Hangar")]
        public string? CapacidadHangar { get; set; }
        public int? AvionId { get; set; }

        [Required]
        [Display(Name = "Propietario")]
        public int? PersonaId { get; set; }

        public byte EstadoHangar { get; set; }

        [Required]
        [Display(Name = "Costo por Hora")]
        public decimal CostoHora { get; set; }

        public virtual Avion? Avion { get; set; }
        public virtual Persona? Persona { get; set; }
    }
}
