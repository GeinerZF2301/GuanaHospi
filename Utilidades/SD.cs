using GuanaHospi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuanaHospi.Utilidades
{
    public class SD
    {
        public List<Usuario> ListaUsuarios()
        {
            return new List<Usuario> {
           new Usuario{ NombreUsuario = "Geiner", CorreoElectronico = "administrador@gmail.com",Contrasenna = "admin2022", Roles = new string[] { "Administrador" } }

           };
        }
    
    }
}
