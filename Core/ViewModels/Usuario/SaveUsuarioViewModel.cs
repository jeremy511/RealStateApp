using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;    

namespace RealState.Core.Application.ViewModels.Usuario
{
    public class SaveUsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Correo { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]


        public string? Fotog { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]

        [DataType(DataType.Upload)]
        public IFormFile Ffile { get; set; }


        public string? Usuer { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Contraseña { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public int? TUser { get; set; }
    }
}
