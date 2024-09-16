using CapstoneServer.DTOs;
using CapstoneServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userDto)
    {
        try
        {
            await _userService.CreateUserAsync(userDto);
            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO loginDto)
    {
        try
        {
            var user = await _userService.LoginUser(loginDto);
            UserDTO userDTO = new(
                user.Id,
                user.Name,
                user.Email,
                user.Username,
                user.Accounts
            );
            return Ok(userDTO);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("User not found"))
				return NotFound(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserUpdateDTO updateDTO)
    {
        try
        {
            var updatedUser = await _userService.UpdateUserAsync(userId, updateDTO);
            return Ok("Details updated successfully");
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("User not found"))
				return NotFound(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update-password/{userId}")]
    public async Task<IActionResult> UpdatePassword(string userId, [FromBody] UpdatePasswordDTO passwordDTO)
    {
        try
        {
            await _userService.UpdatePasswordAsync(userId, passwordDTO);
            return Ok("Password updated successfully");
        }
        catch (Exception ex)
        {   
            if (ex.Message.Contains("User not found"))
				return NotFound(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        try
        {
            await _userService.DeleteUserAsync(userId);
            return Ok("User deleted and accounts deactivated.");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
