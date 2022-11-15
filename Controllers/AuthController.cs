using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using GuanaHospi.Data;
using GuanaHospi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace GuanaHospi.Controllers
{
    public class AuthController : Controller
    {
        public readonly GuanaHospiContext _context;
        
        public AuthController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            bool registrado;
            string mensaje;

            if (usuario.Contrasenna == usuario.ConfirmarContrasenna)
            {
                usuario.Contrasenna = HashearContrasennas(usuario.Contrasenna);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
            }

            using (SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Sp_RegistrarUsuario";
                cmd.Parameters.AddWithValue("NombreUsuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("CorreoElectronico", usuario.CorreoElectronico);
                cmd.Parameters.AddWithValue("Contrasenna", usuario.Contrasenna);
                cmd.Parameters.Add("Registrado", System.Data.SqlDbType.Bit).
                Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", System.Data.SqlDbType.VarChar, 50).
                Direction = System.Data.ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {

                return RedirectToAction("Login", "Auth");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            usuario.Contrasenna = HashearContrasennas(usuario.Contrasenna);
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Sp_ValidarUsuario";
            cmd.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar, 50).Value = usuario.CorreoElectronico;
            cmd.Parameters.Add("@Contrasenna", System.Data.SqlDbType.VarChar, 100).Value = usuario.Contrasenna;

            usuario.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            conn.Close();
            if (usuario.Id != 0)
            {
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                //    new Claim("Correo",usuario.CorreoElectronico)
                //};
                //foreach(string rol in usuario.Roles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, rol));
                //}
                //var claimsidentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsidentity));
                 HttpContext.Session.SetInt32("Usuario", usuario.Id);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();

            }


            
        }
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("Usuario");
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Auth");
            
        }
        public static string HashearContrasennas(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(texto));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        

    }
}
