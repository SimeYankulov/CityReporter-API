using CityReporter.Data.Entities;
using CityReporter.Models.DTOs.UserDtos;
using CityReporter.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityReporter.API.Controllers
{
    [Route("user")]
    public class UserController:ControllerBase
    {
        private readonly IUserService userService;
        private ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ResponseUserDto>>> GetUsers()
        {
            try
            {
                var users = await userService.GetItems();

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
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status400BadRequest,
               "Error retriving data from the database : "+ex.Message);
                
            }
        }
        [HttpGet("{Id:int}")]
        [Authorize(Roles = "Admin")]
        
        public async Task<ActionResult<User>> GetUser(int Id)
        {
            try
            {
                var user = await this.userService.GetItem(Id);

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
                _logger.LogError(ex.ToString());

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
                var result = await this.userService.PostItem(userDto);

                if (!result)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(result);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

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
                var result = await this.userService.Login(credentials);

                if(result.Jwt != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

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
                var result = await this.userService.DeleteItem(id);

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
                _logger.LogError(ex.ToString());

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
                var result = await this.userService.UpdateItem(user);

                if (result)
                {
                    return Ok(result);
                }
                else return BadRequest("Error updating data");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error updating data in the database " + ex.Message);
            }
        }

        [HttpPut("/user/password")]
        [Authorize(Roles = "User")]
        
        public async Task<ActionResult<bool>> UpdateUserPassword([FromBody]LoginDto credentials)
        {
            try
            {
                var result = await this.userService.UpdateUserPassword(credentials);

                if (result)
                {
                    return Ok(result);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error updating data in the database " + ex.Message);
            }
        }
    }
}
