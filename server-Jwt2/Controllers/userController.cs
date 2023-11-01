using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_Jwt2.Dto;
using server_Jwt2.Models;
using server_Jwt2.Repository_s;
using WebApiClientes.Models.Dto;

namespace server_Jwt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly userService userService;
        private readonly ResponseDto response;
        public userController(userService _userService)
        {
            userService = _userService;
            response = new ResponseDto();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(user credentials)
        {
            try
            {
                var userCredentials = await userService.createUser(credentials);
                if (userCredentials!=null)
                {
                    response.IsSuccess = true;
                    response.DisplayMessages = "Welcome";
                    response.Result = userCredentials;
                    return Ok(response);
                }
                response.DisplayMessages = "User or password is Taken!";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.Message;
                return BadRequest(response);
            }
        }//end of register
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(userLoginDto credentials)
        {
            try
            {
                var userCredentials = await userService.getUserDetails(credentials);
                if (userCredentials!=null)
                {
                    response.IsSuccess = true;
                    response.Result = userCredentials;
                    response.DisplayMessages = "Welcome";
                    return Ok(response);
                }
                response.DisplayMessages = "User or password incorrect!";
                return BadRequest(response);
            }
            catch (Exception e)
            {

                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                return BadRequest(response);
            }
        }
    }
}
