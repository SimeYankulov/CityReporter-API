using CityReporter.Data.Data;
using CityReporter.Data.Entities;
using CityReporter.Data.Repositories.Contracts;
using CityReporter.Models.DTOs.UserDtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CityReporter.Data.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly CityReporterDBContext cityReporterDBContext;

        public UserRepository(CityReporterDBContext cityReposrterDBContext) {
            this.cityReporterDBContext = cityReposrterDBContext;
        }

        public async Task<bool> DeleteItem(int Id)
        {
            var user = await this.cityReporterDBContext.Users.FindAsync(Id);

            if (user != null)
            {
                var result = this.cityReporterDBContext.Users.Remove(user);
                await this.cityReporterDBContext.SaveChangesAsync();

                return true;
            }
            else return false;

        }

        public async Task<User> GetItem(int Id)
        {
            var result = await this.cityReporterDBContext.Users.FindAsync(Id);

            if (result != null) return result;
            else return new User();
        }

        public async Task<IEnumerable<User>> GetItems()
        {

            return await this.cityReporterDBContext.Users.ToListAsync();
        }

        public async Task<User> Login(LoginDto credentials)
        {
            var user = await this.cityReporterDBContext.Users.Where(u => u.Email.Equals(credentials.Email)).FirstOrDefaultAsync();

            if(user != null)
            {
                if(user.Password.Equals(PasswordHash(user.Salt, credentials.Password)))
                {
                    return user;
                }
            }
            return new User();
        }

        public async Task<bool> PostItem(User user)
        {

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);


            user.Password = PasswordHash(salt,user.Password);
            user.Salt = salt;

            var result = await this.cityReporterDBContext.AddAsync(user);

            await this.cityReporterDBContext.SaveChangesAsync();

            if (result.Entity != null) { return true; } else return false;
        }

        public async Task<bool> UpdateUserPassword(LoginDto credentials)
        {
            var userToUpdate = await this.cityReporterDBContext.Users
                                                .Where( u => u.Email.Equals(credentials.Email))
                                                .FirstOrDefaultAsync();

            if(userToUpdate != null)
            {

                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

                userToUpdate.Password = PasswordHash(salt,credentials.Password);
                userToUpdate.Salt = salt;

                var result = this.cityReporterDBContext.Users.Update(userToUpdate);

                await this.cityReporterDBContext.SaveChangesAsync();

                if(result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> UpdateItem(User user)
        {
            var userToUpdate = await this.cityReporterDBContext.Users.FindAsync(user.Id);

            if(userToUpdate != null)
            {
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Role = user.Role;
                userToUpdate.Name = user.Name;

                var result = cityReporterDBContext.Users.Update(userToUpdate);

                await this.cityReporterDBContext.SaveChangesAsync();

                if(result != null)
                {
                    return true;
                }
                else { return false; }

            }
            else
            {
                return false;
            }
        }

        private string PasswordHash(byte[] salt,string password)
        {
            return  Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: password!,
                            salt: salt,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8));
        }
    }
}
