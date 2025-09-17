using Codefolio.API.Dto;
using Codefolio.API.Interfaces;

namespace Codefolio.API.Services;

public class AuthService : IAuthService
{
    public AuthDto Register(string username, string email, string password)
    {
        var hashedEmail = HashString(email);
        var hashedPassword = HashString(password);

        return new AuthDto
        {
            Username = username,
            HashedEmail = hashedEmail,
            HashedPassword = hashedPassword
        };
    }

    private static string HashString(string input)
    {
        return BCrypt.Net.BCrypt.HashPassword(input);
    }
}