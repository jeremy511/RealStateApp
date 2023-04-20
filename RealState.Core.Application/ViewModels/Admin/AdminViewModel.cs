using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.ViewModels.Admin
{
    public class AdminViewModel
    {
        public int TotalPropierties { get; set; }
        public int ActiveAgente {get; set;}
        public int UnActiveAgente  { get; set;}
        public int Activedesarrollador { get; set;}
        public int UnActivedesarrollador { get; set; }
        public int ActiveClient {get; set;}
        public int DisableClient{get; set;}



    }
}
