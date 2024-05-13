using CityReporter.API.Entities;
using CityReporter.Models.DTOs.UserDtos;

namespace CityReporter.API.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetItems();
        Task<User> GetItem(int Id);
        Task<User> PostItem(RegisterUserDto user);
        Task<bool> UpdateItem(UpdateUserDto user);
        Task<User> Login(LoginDto credentials);
        Task<bool> DeleteItem(int Id);
        Task<bool> UpdateUserPassword(LoginDto credentials);

    }
}
