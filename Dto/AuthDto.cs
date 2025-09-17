namespace Codefolio.API.Dto;

public class AuthDto
{
    public required string Username { get; set; }
    public required string HashedEmail { get; set; }
    public required string HashedPassword { get; set; }
}