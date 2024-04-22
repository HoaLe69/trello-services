
namespace JwtRoleAuthentication.Services;
using trello_services.Models.Response;
using trello_services.IRepository;
using trello_services.Models;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

public class JwtServices : IJwtServices
{
    // Specify how long until the token expires
    private readonly ILogger<JwtServices> _logger;
    private readonly JwtOptions _jwtOptions;

    public JwtServices(ILogger<JwtServices> logger , IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger;
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(UserResponseVM user)
    {
        // covert text to byte
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] { new Claim("email", user.email) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}