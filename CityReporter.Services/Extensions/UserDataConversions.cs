using CityReporter.Data.Entities;
using CityReporter.Models.DTOs.UserDtos;

namespace CityReporter.Services.Extensions
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

        public static ResponseUserDto ConvertToDto(this User user)
        {
            return new ResponseUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public static IEnumerable<ResponseUserDto> ConvertToDto(this IEnumerable<User> users)
        {
            return (from user in users
                    select new ResponseUserDto
                    {

                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Role = user.Role

                    }).ToList();
        }
    }
}
