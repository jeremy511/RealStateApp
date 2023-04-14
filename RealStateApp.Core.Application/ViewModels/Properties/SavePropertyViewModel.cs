
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace RealState.Core.Application.ViewModels.Properties
{
    public class SavePropertyViewModel
    {
        public int Id { get; set; }
        public string AdsTypeId { get; set; }
        public string SalesId { get; set; }
        public double Price { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        public string Description { get; set; }
        public double Size { get; set; }
        public double BedRooms { get; set; }
        public double BathRooms { get; set; }
        public string ? Upgrates { get; set; }
        public string ? Photos { get; set; }

        public string ? UserId { get; set; }

        public string? Identifier { get; set; }
        public List <IFormFile>  Files { get; set; } 

    }
}
