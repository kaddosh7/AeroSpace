using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeroSpace.Models
{
    public partial class Estadium
    {
        public int IdEstadia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de Entrada *")]
        public DateTime? FechaEntrada { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de Salida *")]
        public DateTime? FechaSalida { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Monto de Estadia *")]
        public decimal? MontoEtadia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nave *")]
        public int? AvionId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Estadia *")]
        public byte EstadoEstadia { get; set; }

        [Required(ErrorMessage = "Seleccione un Hangar")]
        [Display(Name = "Hangar *")]
        public int? HangarId { get; set; }

        public virtual Avion? Avion { get; set; }
        public virtual Hangar? Hangar { get; set; }
    }
}
