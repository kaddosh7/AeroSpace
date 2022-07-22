using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeroSpace.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Empleados = new HashSet<Empleado>();
            Hangars = new HashSet<Hangar>();
            Pilotos = new HashSet<Piloto>();
            Propietarios = new HashSet<Propietario>();
        }

        [Key]
        public int IdPersona { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Núm. Cédula")]
        public string? CedulaPersona { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Nombre de Empleado")]
        public string? NombrePersona { get; set; }

        public byte EstadoPersona { get; set; }

        public string? CargoPersona { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<Hangar> Hangars { get; set; }
        public virtual ICollection<Piloto> Pilotos { get; set; }
        public virtual ICollection<Propietario> Propietarios { get; set; }
    }
}
