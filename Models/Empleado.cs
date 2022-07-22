using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeroSpace.Models
{
    public partial class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Rol de Empleado")]
        public string? RolEmpleado { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Salario $")]
        public decimal? Salario { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Nombre del Empleado")]
        public int? PersonaId { get; set; }

        public byte EstadoEmpleado { get; set; }

        public virtual Persona? Persona { get; set; }
    }
}
