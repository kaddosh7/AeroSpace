using System;
using System.Collections.Generic;

namespace AeroSpace.Models
{
    public partial class Propietario
    {
        public Propietario()
        {
            Avions = new HashSet<Avion>();
        }

        public int IdPropietario { get; set; }
        public string? Rif { get; set; }
        public int? PersonaId { get; set; }
        public byte EstadoPropietario { get; set; }

        public virtual Persona? Persona { get; set; }
        public virtual ICollection<Avion> Avions { get; set; }
    }
}
