namespace RealState.Core.Application.ViewModels.Favourite
{
    public class FavouriteViewModel
    {
        public int Id { get; set; }
        public int ProperyId { get; set; }


        public string Type { get; set; }
        public string SaleType { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double Size { get; set; }
        public double BedRooms { get; set; }
        public double BathRooms { get; set; }
        public string Upgrates { get; set; }
        public string Photos { get; set; }

        public string Identifier { get; set; }

    }
}
