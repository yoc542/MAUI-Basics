using DogHome.Model;

namespace DogHome.Interface
{
    public interface IFormRepository
    {
        Task<List<BuyerForm>> GetAllAsync();
        Task<BuyerForm> GetBySellerIdAsync(int id);
        Task CreateAsync(BuyerForm entity);
        Task DeleteAsync(int id);
    }
}

