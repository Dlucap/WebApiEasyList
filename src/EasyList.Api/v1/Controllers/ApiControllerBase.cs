using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyList.Api.v1.Controllers
{
    [Authorize]
    public class ApiControllerBase : ControllerBase
    {
        protected IdentityUser ObterUsuarioSessao()
        {
            return new IdentityUser
            {
                UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                Email = User?.FindFirst(ClaimTypes.Email)?.Value,               
            };
        }
    }
}
