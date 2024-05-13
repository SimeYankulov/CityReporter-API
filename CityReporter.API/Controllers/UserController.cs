using CityReporter.API.Entities;
using CityReporter.API.Repositories.Contracts;
using CityReporter.Models.DTOs.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityReporter.API.Controllers
{
    [Route("user")]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository userRepository;
        private IConfiguration _config;

        //public ILogger Logger { get; }

        public UserController(IUserRepository userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            _config = config;
            //Logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await this.userRepository.GetItems();

                if(users == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(users);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest,
               "Error retriving data from the database : "+ex.Message);
                
            }
        }
        [HttpGet("{UserId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> GetUser(int UserId)
        {
            try
            {
                var user = await this.userRepository.GetItem(UserId);

                if(user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString());

                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error retriving data from the database:" + ex.Message);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser([FromBody]RegisterUserDto userDto)
        {
            try
            {
                var newUser = await this.userRepository.PostItem(userDto);

                if(newUser == null)
                {
                    return BadRequest();
                }

                var user = await this.userRepository.GetItem(newUser.Id);

                if(user == null)
                {
                    return BadRequest();
                }

                else
                {
                    return Ok(user);
                }


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error writing data in the database:" + ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("/user/login")]
        public async Task<ActionResult<ResponseLogin>> LoginUser(LoginDto credentials)
        {

            try
            {
                var result = await this.userRepository.Login(credentials);

                if (result != null)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                    var credentialss = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                     new Claim(ClaimTypes.Email, result.Email!),
                     new Claim(ClaimTypes.Role, result.Role)
                     };
                    var token = new JwtSecurityToken(
                            _config["Jwt:Issuer"],
                            _config["Jwt:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(15),
                            signingCredentials: credentialss
                        );

                    return new ResponseLogin() {
                        Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                        Role = result.Role };
                
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error retriving data from the database:" + ex.Message);

            }
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            try
            {
                var result = await this.userRepository.DeleteItem(id);

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error removing data in the databse " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<bool>> UpdateUser([FromBody]UpdateUserDto user)
        {
            try
            {
                var result = await this.userRepository.UpdateItem(user);

                if (result)
                {
                    return Ok(result);
                }
                else return BadRequest("Error updating data");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error updating data in the database " + ex.Message);
            }
        }

        [HttpPut("/user/password")]
        //[Authorize(Roles = "User")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> UpdateUserPassword([FromBody]LoginDto credentials)
        {
            try
            {
                var result = await this.userRepository.UpdateUserPassword(credentials);

                if (result)
                {
                    return Ok(result);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error updating data in the database " + ex.Message);
            }
        }
    }
}
