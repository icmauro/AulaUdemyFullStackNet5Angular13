using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Extensions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProEventos.Aula.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService,
                                 ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUsuario/")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();

                var user = await _userService.GetUserByNomeAsync(userName);
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuário. Erro: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUser([FromBody] UserDto userDto)
        {
            try
            {
                if (await _userService.UserExists(userDto.UserName))
                    return Conflict("Usuário já existe.");

                var user = await _userService.CreatUserAsync(userDto);

                if (user is not null)
                    return Ok(user);

                return BadRequest("Usuário não criado, tente novamente.");


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar registrar usuário. Erro: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _userService.GetUserByNomeAsync(userLoginDto.UserName);
                if (user is null) return Unauthorized("Usuário e/ou senha inválidos.");

                var result = await _userService.CheckUserPasswordAsync(user, userLoginDto.Password);
                if (!result.Succeeded) return Unauthorized("Usuário e/ou senha inválidos.");


                return Ok(new
                {
                    userName = user.UserName,
                    user.PrimeiroNome,
                    token =  _tokenService.CreateToken(user).Result

                });


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar logar usuário. Erro: {ex.Message}");
            }
        }

        [HttpPut("Atualizar")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userService.GetUserByNomeAsync(User.GetUserName());
                if (user is null) return Unauthorized("Usuário e/ou senha inválidos.");

                var userReturn = await _userService.UpdateUserAsync(userUpdateDto);

                if (userReturn is null)
                    return NoContent();

                return Ok(userReturn);


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }
    }
}
