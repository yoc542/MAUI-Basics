using DogHome.Repositories;
namespace DogHome.Model
{
    public class BuyerForm : IEntityKey
    {
        public int Id { get; set; }
        public DateTime VisitTime { get; set; }
        public string? BuyerName { get; set; }
        public string? BuyerContact { get; set; }
        public string? Query { get; set; }
        public string? InterestedDogs { get; set; }

        public int SellerId { get; set; }
    }
}
