using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using NuGet.Protocol;
using UrlShort.Models.Identity;

namespace UrlShort.Controllers;

[ApiController]
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
 
    public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    
    [Route("[controller]/register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]UserDto User)
    {
        var user = new IdentityUser(User.UserName);
        var result = await _userManager.CreateAsync(user, User.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }            
        }

        var token = await Token(user);
        
        return Ok(token);
    }
    
    [HttpPost]
    [Route("[controller]/token")]
    public async Task<string?> Token(IdentityUser user)
    {
        var identity = await GetIdentityAsync(user);
        if (identity == null) return null;
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: identity.Claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };

        return response.ToJson();
    }
    
    private async Task<ClaimsIdentity> GetIdentityAsync(IdentityUser user)
    {
        var person = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
        if (person == null) return null;

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}

