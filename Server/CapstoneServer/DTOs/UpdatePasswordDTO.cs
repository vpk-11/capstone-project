using System;

namespace CapstoneServer.DTOs;

public class UpdatePasswordDTO
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
