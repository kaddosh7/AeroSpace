using System.ComponentModel.DataAnnotations;

namespace AeroSpace.Models
{
    public class Usuarioscs
    {
        public int IdUsuario { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string Clave { get; set; }

        public string ConfirmClave { get; set; }
    }
}
