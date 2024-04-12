using DogHome.Model;

namespace DogHome.Interface
{
    public interface IBreedRepository
    {
        Task<List<Breed>> GetAllAsync();
        Task<Breed> GetByBreedAsync(string BreedName);
        Task<Breed> GetByIdAsync(int id);
        Task CreateAsync(string name);
        Task UpdateAsync(Breed entity);
        Task DeleteAsync(int id);
    }
}
