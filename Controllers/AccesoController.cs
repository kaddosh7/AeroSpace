using Microsoft.AspNetCore.Mvc;
using AeroSpace.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AeroSpace.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Data Source=Evangelist; Database=AeroSpace; Integrated Security=True;";

        private readonly AeroSpaceContext _context;

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AccesoController(AeroSpaceContext context)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _context = context;
        }

        public object SqlDbType { get; private set; }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Registrar(Usuarioscs usuario)
        {
            bool registrado;
            string mensaje;

            if (usuario.Clave == usuario.ConfirmClave)
            {
                usuario.Clave = ConvertirSha256(usuario.Clave);
            }
            else
            {
                ViewData["Mensaje"] = "Las contrasenias no coninciden";
                return View();
            }

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", con);
                cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("Clave", usuario.Clave);
                cmd.Parameters.Add("Registrado", System.Data.SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", System.Data.SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
                {
                    return RedirectToAction("Login", "Acceso");
                } else
                {
                    return View();
                }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuarioscs usuario)
        {
            if (usuario.Clave != null)
            {
                usuario.Clave = ConvertirSha256(usuario.Clave);
            }

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", con);
                cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("Clave", usuario.Clave);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                usuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if (usuario.IdUsuario != 0)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.Correo)
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Mensaje"] = "Usuario o Clave incorrecto";
                    return View();
                }
            
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
                return Sb.ToString();
            }
        }

        public async Task<IActionResult> CerrarSesion(object Session)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }



    }
}
