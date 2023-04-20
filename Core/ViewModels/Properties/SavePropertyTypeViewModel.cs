﻿

using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModels.Properties
{
    public class SavePropertyTypeViewModel
    {

        public int ? Id { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        public string Description { get; set; }
       
    }
}
