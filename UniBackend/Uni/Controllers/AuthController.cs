using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniBackend.Controllers.ApiEndpoints;
using UniBackend.Controllers.Dtos;
using UniBackend.Models;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    [HttpPost(ApiEndpoints.Auth.Login)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        var user = await _db.User.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return Ok(new { token = jwt });
    }

    [HttpPost(ApiEndpoints.Auth.Register)]
    public async Task<IActionResult> Register([FromBody] UserLoginDto request)
    {
        var existingUserModel = await _db.User.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (existingUserModel != null)
            return BadRequest("Username already exists.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new UserModel
        {
            Username = request.Username,
            PasswordHash = hashedPassword
        };

        _db.User.Add(user);
        await _db.SaveChangesAsync();

        return Ok("UserModel registered successfully.");
    }

    [HttpPatch(ApiEndpoints.Auth.ToggleUserIsActive)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleUserActiveStatus([FromQuery] int id)
    {
        var user = await _db.User.FindAsync(id);
        if (user == null)
            return NotFound("User not found.");

        user.IsActive = !user.IsActive;
        await _db.SaveChangesAsync();

        return Ok(new { user.Id, user.Username, user.IsActive });
    }
}