using CityReporter.Models.DTOs.UserDtos;

namespace CityReporter.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<ResponseUserDto>> GetItems();
        Task<ResponseUserDto> GetItem(int Id);
        Task<bool> PostItem(RegisterUserDto user);
        Task<bool> UpdateItem(UpdateUserDto user);
        Task<ResponseLogin> Login(LoginDto credentials);
        Task<bool> DeleteItem(int Id);
        Task<bool> UpdateUserPassword(LoginDto credentials);
    }
}
