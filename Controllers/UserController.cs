using Microsoft.AspNetCore.Mvc;
using Codefolio.API.Dto;
using System.Text.RegularExpressions;
using Codefolio.API.Interfaces;

namespace Codefolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IAuthService _authService, AutoMapper.IMapper _mapper) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (isValid, errorMessage) = Validator.IsUserDtoValid(userDto);
        if (!isValid)
            return BadRequest(errorMessage);

        var authDto = _mapper.Map<AuthDto>(_authService.Register(userDto.Username, userDto.Email, userDto.Password));

        return Ok(authDto);
    }
}

public partial class Validator
{
    public static (bool, string) IsUserDtoValid(UserDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Username) ||
            string.IsNullOrWhiteSpace(userDto.Email) ||
            string.IsNullOrWhiteSpace(userDto.Password))
        {
            return (false, "All fields are required.");
        }

        if (!EmailRegex().IsMatch(userDto.Email))
        {
            return (false, "Invalid email format.");
        }

        if (userDto.Password.Length < 6)
        {
            return (false, "Password must be at least 6 characters long.");
        }

        return (true, string.Empty);
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
}