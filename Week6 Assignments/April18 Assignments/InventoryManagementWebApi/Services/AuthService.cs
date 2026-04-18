using InventoryManagementWebApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public string Register(User user)
    {
        var exists = _context.Users.Any(u => u.Email == user.Email);
        if (exists) return "User already exists";

        _context.Users.Add(user);
        _context.SaveChanges();

        return "User Registered";
    }

    public string Login(LoginModel model)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

        if (user == null) return null;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.UserRole),
            new Claim("UserId", user.UserId.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("InventorySecretKey@1234567890123456"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}