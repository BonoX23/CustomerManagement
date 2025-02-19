using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Auth
{
    [Route("api/v1")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// Autenticar
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Autenticate([FromBody] AuthDto auth)
        {
            return Ok(await _service.AutenticateAsync(auth));
        }

        /// <summary>
        /// Atualizar Senha
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("auth/update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] UserDto auth)
        {
            await _service.UpdateUserPasswordAsync(UserLoggedId, auth);

            return Ok(new Tuple<string>("Senha atualiza com sucesso"));
        }
    }
}