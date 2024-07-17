using AuthenticationServer.Api.Services.IdentityRepository;
using AuthenticationServer.Api.Services.TokenGenerator;
using ContactsNotebook.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationServer.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class AuthenticationController(IIdentityRepository repository, IAccessTokenGenerator tokenGenerator) : Controller
    {
        private readonly IIdentityRepository _repository = repository;
        private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doesUserExist = await _repository.DoesUserExistAsync(registerViewModel.Email!);
            if (doesUserExist)
            {
                return Conflict(new { ErrorMessage = "Юзер с таким email'ом уже существует." });
            }
            string[] roles = registerViewModel.IsAdmin ? ["Administrator", "User"] : ["User"];
            var newUser = await _repository.CreateUserAsync(registerViewModel.Email!, registerViewModel.Email!, registerViewModel.Password!, roles);
            if (newUser == null)
            {
                return StatusCode(500, new { ErrorMessage = "При создании пользователя возникла ошибка." });
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var doesExist = await _repository.DoesUserExistAsync(id);
            if (!doesExist)
            {
                return BadRequest(new { Error = "Невозможно удалить: Пользователь с таким id не найден." });
            }
            var result = await _repository.DeleteUserAsync(id);
            if (!result)
            {
                return StatusCode(500, new { Error = "При удалении пользователя возникла ошибка." });
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doesUserExist = await _repository.DoesUserExistAsync(loginViewModel.Email!);
            if (!doesUserExist)
            {
                return Conflict(new { ErrorMessage = "Юзер с таким email'ом не существует." });
            }
            var user = await _repository.LoginAsync(loginViewModel.Email!, loginViewModel.Password!, loginViewModel.RememberMe);
            if (user == null)
            {
                return StatusCode(500, new { ErrorMessage = "При входе пользователя возникла ошибка." });
            }
            var roles = await _repository.GetRolesAsync(user);
            var accessToken = _tokenGenerator.GenerateToken(user, roles);
            var result = await _repository.RefreshToken(user, accessToken);
            if (!result)
            {
                return StatusCode(500, new { ErrorMessage = "При обновлении токена в системе произошла ошибка." });
            }
            return Ok(new TokenResponse(accessToken));
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var authorizationHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authorizationHeader == null)
            {
                return BadRequest();
            }
            var accessToken = authorizationHeader.Substring(7);
            var user = _repository.FindUserByToken(accessToken);
            if (user == null)
            {
                return BadRequest();
            }
            await _repository.RefreshToken(user, "");
            await _repository.LogOutAsync();
            return Ok();
        }
    }
}
