using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TicketingSystemIT.Entities.Models;
using TicketingSystemIT.Entities.Resources;

namespace TicketingSystemIT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IConfiguration configuration;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
           IConfiguration configuration)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.configuration = configuration;
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] SaveUserResource userAccount)
    {
        var userFromDb = userManager.FindByNameAsync(userAccount.UserName).Result;
        var result = signInManager.CheckPasswordSignInAsync(userFromDb, userAccount.Password, false).Result;


        if (result.Succeeded)
        {
            var tokenString = GenerateJSONWebToken(userFromDb);
            return Ok(new
            {
                userFromDb.Id,
                userFromDb.Email,
                userFromDb.UserName,
                Roles = userManager.GetRolesAsync(userFromDb).Result,
                tokenString
            });
        }

        return Unauthorized();
    }
    [Authorize(Policy = Policies.Supervisor)]
    [HttpPost("create/ITEmployee")]
    public IActionResult RegisterITEmployee([FromBody] SaveUserResource userAccount)
    {
        return RegisterEmployee(userAccount, true);
    }
    [Authorize(Policy = Policies.Supervisor)]
    [HttpPost("create/Employee")]
    public IActionResult RegisterEmployee([FromBody] SaveUserResource userAccount)
    {
        return RegisterEmployee(userAccount, false);
    }
    private IActionResult RegisterEmployee(SaveUserResource userAccount, bool isITEmployee)
    {
        var user = new User()
        {
            UserName = userAccount.UserName
        };
        userManager.CreateAsync(user, userAccount.Password).Wait();

        var registeredUser = userManager.FindByNameAsync(user.UserName).Result;
        userManager.AddToRoleAsync(registeredUser, Policies.ITEmployee).Wait();

        if (isITEmployee)
            userManager.AddToRoleAsync(registeredUser, Policies.Employee).Wait();

        return Ok(new
        {
            registeredUser.Id,
            registeredUser.UserName,
            Roles = userManager.GetRolesAsync(registeredUser).Result,
        });
    }

    private string GenerateJSONWebToken(User user)
    {
        var claims = new List<Claim>();
        var roles = userManager.GetRolesAsync(user).Result;
        claims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = credentials
        };
        return tokenHandler.WriteToken(tokenHandler.CreateToken(token));
    }
}
