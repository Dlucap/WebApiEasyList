using EasyList.Api.ApiModels;
using EasyList.Api.Extensions;
using EasyList.Api.Services;
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
        /// <returns> Mensagem de sucesso</returns>
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

            // Gerar token de confirmação de email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = System.Net.WebUtility.UrlEncode(token);
            
            // Criar link de confirmação
            var confirmationLink = $"{Request.Scheme}://{Request.Host}/api/v1/Auth/confirmar-email?userId={user.Id}&token={encodedToken}";
            
            // Enviar email de confirmação
            await _emailService.SendEmailConfirmationAsync(user.Email, user.UserName, confirmationLink);

            return Ok(new { 
                message = "Usuário registrado com sucesso. Por favor, verifique seu email para confirmar sua conta.",
                userId = user.Id
            });
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

            var user = await _userManager.FindByNameAsync(loginUser.UserName);
            
            if (user != null && !user.EmailConfirmed)
                return BadRequest("Email não confirmado. Por favor, confirme seu email antes de fazer login.");

            var result = await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, true);
         
            if (!result.Succeeded)
                return BadRequest("Usuário ou senha inválidos");

            return Ok(await GerarJwt(loginUser.UserName));
        }

        /// <summary>
        /// Confirmar Email
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <param name="token">Token de confirmação</param>
        /// <returns> Mensagem de sucesso</returns>
        /// <response code="200"> Email confirmado com sucesso </response>
        /// <response code="400"> Token inválido ou expirado </response>
        [HttpGet("confirmar-email")]
        public async Task<ActionResult> ConfirmarEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return BadRequest("Usuário ou token inválido");

            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
                return BadRequest("Usuário não encontrado");

            if (user.EmailConfirmed)
                return Ok("Email já confirmado anteriormente");

            var decodedToken = System.Net.WebUtility.UrlDecode(token);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
                return BadRequest("Erro ao confirmar email. Token inválido ou expirado.");

            return Ok("Email confirmado com sucesso! Você já pode fazer login.");
        }

        /// <summary>
        /// Reenviar Email de Confirmação
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <returns> Mensagem de sucesso</returns>
        /// <response code="200"> Email reenviado com sucesso </response>
        /// <response code="400"> Requisição inválida </response>
        [HttpPost("reenviar-confirmacao")]
        public async Task<ActionResult> ReenviarEmailConfirmacao([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email é obrigatório");

            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
                return BadRequest("Usuário não encontrado");

            if (user.EmailConfirmed)
                return Ok("Email já confirmado anteriormente");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = System.Net.WebUtility.UrlEncode(token);
            
            var confirmationLink = $"{Request.Scheme}://{Request.Host}/api/v1/Auth/confirmar-email?userId={user.Id}&token={encodedToken}";
            
            await _emailService.SendEmailConfirmationAsync(user.Email, user.UserName, confirmationLink);

            return Ok("Email de confirmação reenviado com sucesso. Por favor, verifique sua caixa de entrada.");
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
        #endregion Métodos privados
    }
}
