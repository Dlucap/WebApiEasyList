using EasyList.Api.ApiModels;
using EasyList.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace EasyList.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Registrar Novo Usuário
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns> Token de Autenticação</returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = registerUser.UserName,
                NormalizedUserName = registerUser.Name,
                Email = registerUser.Email,
                EmailConfirmed = false
            };

            var verificaUserNameJaExiste = await _userManager.FindByNameAsync(user.UserName);
            
            if(verificaUserNameJaExiste is not null)
                return BadRequest($"Usuário ja cadastrado com o nome {user.UserName}");

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);

            return Ok(await GerarJwt(registerUser.Email));
        }

        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns> Token de Autenticação</returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
         
            if (!result.Succeeded)
                return BadRequest("Usuário ou senha inválidos");

            return Ok(await GerarJwt(loginUser.Email));
        }

        #region Métodos privados
        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(await _userManager.GetClaimsAsync(user));

            //autentication sucessful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        }
        #endregion Métodos privados
    }
}
