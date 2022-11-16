using GuanaHospi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuanaHospi.Utilidades
{
    public class SD
    {
        public List<Usuario> ListaUsuario()
        {

            return new List<Usuario>
            {
                new Usuario{ NombreUsuario ="geiner", CorreoElectronico = "admin@gmail.com", Contrasenna= "admin123" , Roles = new string[]{"Administrador"} },
                new Usuario{ NombreUsuario ="melany", CorreoElectronico = "secretario@gmail.com", Contrasenna= "secre123" , Roles = new string[]{"Secretario"} },
                new Usuario{ NombreUsuario ="gerald", CorreoElectronico = "doctor@gmail.com", Contrasenna= "doctor123" , Roles = new string[]{"Doctor"} },
                new Usuario{ NombreUsuario ="ivan", CorreoElectronico = "usuario@gmail.com", Contrasenna = "usuario123" , Roles = new string[]{"Usuario"} }

            };

        }
        public Usuario ValidarUsuario(string _correo, string _clave)
        {

            return ListaUsuario().Where(item => item.CorreoElectronico == _correo && item.Contrasenna == _clave).FirstOrDefault();

        }


    }
}
