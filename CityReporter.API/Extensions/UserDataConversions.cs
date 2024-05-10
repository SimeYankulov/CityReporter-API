using CityReporter.API.Entities;
using CityReporter.Models.DTOs;

namespace CityReporter.API.Extensions
{
    public static class UserDataConversions
    {
        public static User ConvertToEntity(this RegisterUserDto user)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
        }
        public static User ConvertToEntity(this UpdateUserDto user)
        {
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
        }
    }
}
