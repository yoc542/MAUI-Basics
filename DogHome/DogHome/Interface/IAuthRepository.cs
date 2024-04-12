using DogHome.Model;

namespace DogHome.Interface
{
    public interface IAuthRepository
    {
        Task<List<Auth>> GetAllAsync();
        Task<Auth> GetByEmailAsync(string email);
        Task CreateAsync(Auth entity);
        Task UpdateAsync(Auth entity);
        Task DeleteAsync(int id);
    }
}
