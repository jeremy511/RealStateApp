
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModels.Agents
{
    public class SaveAgentViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


        public IFormFile? ProfilePic { get; set; }
        public string? Photo { get; set; }


        [RegularExpression(@"^\+[1-1]\([8-8]\d{1}[9-9]\)-\d{3}\-\d{4}$", ErrorMessage = "Telefono no valido Ejemplo de telefono +1(8x9)-xxx-xxxx")]
        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }


    }
}
