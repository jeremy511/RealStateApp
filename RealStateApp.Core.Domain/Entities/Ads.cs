using RealState.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Domain.Entities
{
    public class Ads : AuditableBaseEntity
    {
        public int AdsTypeId { get; set; }
        public int SalesId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double Size { get; set; }
        public double BedRooms { get; set; }
        public double BathRooms { get; set; }
        public string Upgrates { get; set; }
        public string Photos { get; set; }

        public string UserId { get; set; }


        public string Identifier { get; set; }

        public ICollection<FavouriteProperties> FavouriteProperties { get; set; }
        public Sales Sales { get; set; }
        public AdsType AdsType { get; set; }


    }
}
