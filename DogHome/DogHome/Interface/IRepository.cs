using DogHome.Model;

namespace DogHome.Interface
{
    public interface IRepository
    {
        Task<List<Dog>> GetAllAsync();
        Task<Dog> GetByIdAsync(int id);
        Task CreateAsync(Dog entity);
        Task UpdateAsync(Dog entity);
        Task DeleteAsync(int id);
    }
}
