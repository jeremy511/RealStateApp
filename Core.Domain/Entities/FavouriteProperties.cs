using RealState.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Domain.Entities
{
    public class FavouriteProperties : AuditableBaseEntity
    {
        public string UserName { get; set; }
        public int PropetyId { get; set; }
        public Ads Ads { get; set; }
    }
}
