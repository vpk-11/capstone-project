namespace CapstoneServer.DTOs;

public class UserUpdateDTO
{
    public string? Name { get; set; } // Optional
    public string? Email { get; set; } // Optional
    public string? Username { get; set; } // Optional
    public string? Password { get; set; } // Optional, leave empty if not updating password
}
