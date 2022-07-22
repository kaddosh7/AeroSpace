using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeroSpace.Models
{
    public partial class Avion
    {
        public Avion()
        {
            Estadia = new HashSet<Estadium>();
            Hangars = new HashSet<Hangar>();
            Modelos = new HashSet<Modelo>();
            Vuelos = new HashSet<Vuelo>();
        }

        public int IdAvion { get; set; }

        [MaxLength(14, ErrorMessage = "La longitud debe ser menor o igual a 14 caracteres")]
        public string? Siglas { get; set; }
        public double? Capacidad { get; set; }
        public string? TipoAvion { get; set; }
        public int? PropietarioId { get; set; }

        [Required(ErrorMessage = "Expecifique Numero de Motores")]
        public int? NumMotores { get; set; }
        public byte EstadoAvion { get; set; }

        public virtual Propietario? Propietario { get; set; }
        //public virtual Persona? Persona { get; set; }
        public virtual ICollection<Estadium> Estadia { get; set; }
        public virtual ICollection<Hangar> Hangars { get; set; }
        public virtual ICollection<Modelo> Modelos { get; set; }
        public virtual ICollection<Vuelo> Vuelos { get; set; }
    }
}
