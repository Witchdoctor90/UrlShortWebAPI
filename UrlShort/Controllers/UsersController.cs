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
    private readonly RoleManager<IdentityRole> _roleManager;
 
    public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
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
    [Route("[controller]/login")]
    public async Task<IActionResult> Login(UserDto user)
    {
        var identity = await _userManager.FindByNameAsync(user.UserName);
        if (identity == null) return BadRequest("Invalid login or password");
        var result = await _signInManager.CheckPasswordSignInAsync(identity, user.Password, false);
        if (!result.Succeeded) return BadRequest("Invalid login or password");

        var token = await Token(identity);
        if (token == null) return BadRequest("Login error, please try again");
        return Ok(token);
    }


    [HttpPost]
    [Route("[controller]/isAdmin")]
    public async Task<bool> IsAdministrator([FromBody] string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return await _userManager.IsInRoleAsync(user, "Administrator");
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
        return encodedJwt.ToJson();
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

