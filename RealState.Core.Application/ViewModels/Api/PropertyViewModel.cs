namespace RealState.Core.Application.ViewModels.Api
{
    public class PropertyViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Type { get; set; }
        public string SaleType { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double Size { get; set; }
        public double BedRooms { get; set; }
        public double BathRooms { get; set; }
        public string Upgrates { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
    }
}