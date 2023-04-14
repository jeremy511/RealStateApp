using RealState.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Domain.Entities
{
    public class Sales : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
     
        public ICollection<Ads> Ads { get; set; }

    }
}

