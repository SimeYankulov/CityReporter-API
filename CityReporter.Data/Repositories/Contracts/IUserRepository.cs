using CityReporter.Data.Entities;
using CityReporter.Models.DTOs.UserDtos;

namespace CityReporter.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetItems();
        Task<User> GetItem(int Id);
        Task<bool> PostItem(User user);
        Task<bool> UpdateItem(User user);
        Task<User> Login(LoginDto credentials);
        Task<bool> DeleteItem(int Id);
        Task<bool> UpdateUserPassword(LoginDto credentials);

    }
}
