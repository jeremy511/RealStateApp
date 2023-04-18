
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModels.Users
{
    public class SaveAdminViewModel
    {

        public string ? Id { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?#&])[A-Za-z\d$@$!%*#?&]{7,100}[^'\s]", ErrorMessage = "La contraseña debe tener minimo 8 digitos, 1 letra mayuscula, 1 caracter especial($@$!%*?.#&) y al menos 1 numero. Ejemplo Pass123#")]
        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas deben coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "*Obligado*")]
        public string IdCard { get; set; }

        public string ? UserType { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }


    }
}
