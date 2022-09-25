using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NLayerApp.WEB.Classes;

namespace NLayerApp.WEB.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("GetJwtToken")]
        public async Task<IActionResult> GetJwtToken()
        {
            var claims = await Task.FromResult(new List<Claim> { new Claim(ClaimTypes.Name, "admin") });

            // creating jwt token
            var jwt = await Task.FromResult(
                new JwtSecurityToken
                (
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256))
                );
            return await Task.FromResult(Ok(new JwtSecurityTokenHandler().WriteToken(jwt)));
        }
    }
}
