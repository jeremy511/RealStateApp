

using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModels.Properties
{
    public class SaveUpgradeViewModel
    {
        public int ? Id { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        public string Description { get; set; }
       
    }
}
