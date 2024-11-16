using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quiz_2.Data;

public interface IJwtTokenService
{
	string GenerateToken(string email);
}
public class JwtTokenService : IJwtTokenService
{
	private readonly IConfiguration _config;

	public JwtTokenService(IConfiguration config) => _config = config;

	public string GenerateToken(string email)
	{
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		Claim[] claims = [new Claim(ClaimTypes.Email, email),];

		var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			audience: _config["Jwt:Audience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"])),
			signingCredentials: credentials
			);
			

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}