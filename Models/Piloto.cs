using System;
using System.Collections.Generic;

namespace AeroSpace.Models
{
    public partial class Piloto
    {
        public int IdPiloto { get; set; }
        public string? LicenciaPiloto { get; set; }
        public double? HorasVuelo { get; set; }
        public int? PersonaId { get; set; }
        public DateTime? FechaRev { get; set; }
        public byte EstadoPiloto { get; set; }

        public virtual Persona? Persona { get; set; }
    }
}
