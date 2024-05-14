using CityReporter.Data.Repositories.Contracts;
using CityReporter.Models.DTOs.UserDtos;
using CityReporter.Services.Contracts;
using CityReporter.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityReporter.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository,IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this._configuration = configuration;
        }
        public async Task<bool> DeleteItem(int Id)
        {
            return await userRepository.DeleteItem(Id);
        }

        public async Task<ResponseUserDto> GetItem(int Id)
        {
            var user = await userRepository.GetItem(Id);

            return user.ConvertToDto();
        }

        public async Task<IEnumerable<ResponseUserDto>> GetItems()
        {
            var users = await userRepository.GetItems();

            return users.ConvertToDto();
        }

        public async Task<ResponseLogin> Login(LoginDto credentials)
        {
            var user = await this.userRepository.Login(credentials);

            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var credentialss = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                     new Claim(ClaimTypes.Email, user.Email!),
                     new Claim(ClaimTypes.Role, user.Role)
                     };
                var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: credentialss
                    );

                return new ResponseLogin()
                {
                    Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    Role = user.Role
                };
            }
            else
            {
                return new ResponseLogin();
            }
        }

        public async Task<bool> PostItem(RegisterUserDto user)
        { 
            return await this.userRepository.PostItem(user.ConvertToEntity());
        }

        public async Task<bool> UpdateItem(UpdateUserDto user)
        {
            return await this.userRepository.UpdateItem(user.ConvertToEntity());
        }

        public async Task<bool> UpdateUserPassword(LoginDto credentials)
        {
            return await this.userRepository.UpdateUserPassword(credentials);
        }
    }
}
