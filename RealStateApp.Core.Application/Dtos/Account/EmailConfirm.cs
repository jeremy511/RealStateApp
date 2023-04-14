using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.Dtos.Account
{
    //Email confimado, devuelve el id y un token que valide
    public class EmailConfirm
    {
        public string UserId { get; set;}
        public int Token { get; set; }
    }
}
