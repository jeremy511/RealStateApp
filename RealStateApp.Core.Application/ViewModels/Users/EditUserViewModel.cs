
using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModels.Users
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        public string UserType { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Upload)]
        public double? Fotog { get; set; }
        public string? Pass { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
       
    }
}
