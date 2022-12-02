using GSC_FinalProject.Data;
using GSC_FinalProject.Dto;
using GSC_FinalProject.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSC_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtHandler _jwtHandler;

        public UserController(IJwtHandler jwtHandler, IUnitOfWork uow)
        {
            _uow = uow;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO userDTO)
        {
            var response = new UserResponseDTO();
            var user = _uow.UsersRepository.Login(userDTO.Username, userDTO.Password);

            if (user is null)
            {
                response.Message = "Username and Password do not match";
                response.Success = false;
                return Unauthorized(response);
            }

            response.Message = "You are successfully logged in";
            response.Success = true;
            response.Token = _jwtHandler.GenerateToken(userDTO, user.Role);
            return Ok(response);
        }
    }
}
