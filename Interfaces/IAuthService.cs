using Codefolio.API.Dto;

namespace Codefolio.API.Interfaces;

public interface IAuthService
{
    AuthDto Register(string username, string email, string password);
}