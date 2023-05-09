using EasyList.Api.ApiModels;
using EasyList.Api.Extensions;
using EasyList.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
        private readonly IEmailService _emailService;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _emailService = emailService;
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

            if (verificaUserNameJaExiste is not null)
                return BadRequest($"Usuário ja cadastrado com o nome {user.UserName}");

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var origin = Request.Headers["origin"];

            var validacaoEmail = await VerificacaoEmail(user, origin);
            await _emailService.EnviarEmailConfirmacaoAsync(user.Email, validacaoEmail);

            await _signInManager.SignInAsync(user, false);

            var token = await GerarJwt(user.UserName);

            var response = new
            {
                token = token,
                validade = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            };
        
            return Ok(response);
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

            var result = await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, true);
         
            if (!result.Succeeded)
                return BadRequest("Usuário ou senha inválidos");

            var token = await GerarJwt(loginUser.UserName);

            var response = new 
            {
                token = token,
                validade = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            };

            return Ok(response);
        }

        /// <summary>
        /// Confirmar email cadastrado
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns>Conta confirmada</returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
                return BadRequest($"Usuário não encontrado.");

            if (user.EmailConfirmed)
                return Ok($"Conta confirmada");

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
          
            if (result.Succeeded)
                return Ok($"Conta confirmada");
            else
                return BadRequest($"An error occured while confirming {user.Email}.");
        }

        #region Métodos privados
        private async Task<string> GerarJwt(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
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

        private async Task<string> VerificacaoEmail(IdentityUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/v1/Auth/confirm-email";
            
            if (origin.EndsWith("/"))
               origin = origin.Remove(origin.Length - 1);

            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }
        #endregion Métodos privados
    }
}
