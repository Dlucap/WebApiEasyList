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

            var result = await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, true);
         
            if (!result.Succeeded)
                return BadRequest("Usuário ou senha inválidos");

            return Ok(await GerarJwt(loginUser.UserName));
        }

        /// <summary>
        /// Login Social (Google ou Instagram)
        /// </summary>
        /// <param name="provider">Provedor de autenticação (Google ou Instagram)</param>
        /// <param name="returnUrl">URL de retorno após autenticação</param>
        /// <returns> Redireciona para o provedor de autenticação</returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpGet("external-login")]
        public IActionResult ExternalLogin([FromQuery] string provider, [FromQuery] string returnUrl = null)
        {
            if (string.IsNullOrEmpty(provider))
                return BadRequest("Provider não especificado");

            if (!string.IsNullOrEmpty(returnUrl) && !IsLocalUrl(returnUrl))
                return BadRequest("URL de retorno inválida");

            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        /// <summary>
        /// Callback de Login Social
        /// </summary>
        /// <param name="returnUrl">URL de retorno</param>
        /// <returns> Token de Autenticação</returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpGet("external-login-callback")]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return BadRequest("Erro ao carregar informações de login externo");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            
            if (result.Succeeded)
            {
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                return Ok(await GerarJwt(user.UserName));
            }
            
            if (result.IsLockedOut)
            {
                return BadRequest("Conta bloqueada");
            }

            return await CriarOuVincularUsuarioExterno(info);
        }

        /// <summary>
        /// Lista os provedores de login externo disponíveis
        /// </summary>
        /// <returns> Lista de provedores</returns>
        /// <response code="200"> Sucesso </response>
        [HttpGet("external-login-providers")]
        public async Task<ActionResult> GetExternalLoginProviders()
        {
            var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();
            var providers = schemes.Select(s => new { Name = s.Name, DisplayName = s.DisplayName }).ToList();
            return Ok(providers);
        }

        #region Métodos privados
        private async Task<ActionResult> CriarOuVincularUsuarioExterno(ExternalLoginInfo info)
        {
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email não fornecido pelo provedor");

            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                
                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                    return BadRequest(createResult.Errors);
            }

            var addLoginResult = await _userManager.AddLoginAsync(user, info);
            if (!addLoginResult.Succeeded)
                return BadRequest(addLoginResult.Errors);

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok(await GerarJwt(user.UserName));
        }

        private bool IsLocalUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            return Uri.TryCreate(url, UriKind.Relative, out _) ||
                   (Uri.TryCreate(url, UriKind.Absolute, out var absoluteUri) && 
                    string.Equals(Request.Host.Host, absoluteUri.Host, StringComparison.OrdinalIgnoreCase));
        }

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
