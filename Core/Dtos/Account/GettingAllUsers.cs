using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.Dtos.Account
{
    //Busca los usuarios activados y desactivados
    public class GettingAllUsers
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }
}
