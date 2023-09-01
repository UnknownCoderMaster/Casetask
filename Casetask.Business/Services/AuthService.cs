using Casetask.Business.Exceptions;
using Casetask.Business.Extensions;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace Casetask.Business.Services;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IConfiguration _configuration;


    public AuthService(IGenericRepository<User> userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    public async Task<string> GenerateToken(string email, string password)
	{
        List<User> users = await _userRepository.GetFilteredAsync(
            new Expression<Func<User, bool>>[] {
                u => u.Email == email && u.Password.Equals(password.Encrypt())
            },
            null, null);

        if (users is null)
            throw new UserException(400, "Login or Password is incorrect");

        var user = users.First();

        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            expires: DateTime.Now.AddHours(int.Parse(_configuration["JWT:Expire"])),
            claims: new List<Claim>
            {
                new Claim( JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim( ClaimTypes.Role, user.Role.ToString())
            },
            signingCredentials: new SigningCredentials(
                key: authSigningKey,
                algorithm: SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
