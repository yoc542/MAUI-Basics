using DogHome.Repositories;

namespace DogHome.Model
{
    public class Breed : IEntityKey
    {
        public int Id { get; set; }
        public string BreedName { get; set; }
    }
}
