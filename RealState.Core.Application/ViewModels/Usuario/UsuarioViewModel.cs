using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.ViewModels.Usuario
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Fotog { get; set; }
        public string? Usuer { get; set; }
        public string? Contraseña { get; set; }
        public int? TUser { get; set; }
    }
}
