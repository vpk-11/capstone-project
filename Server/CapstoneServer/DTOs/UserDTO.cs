using System;
using CapstoneServer.Models;

namespace CapstoneServer.DTOs;
public class UserDTO
{
    public UserDTO(string id, string name, string email, string username, List<AccountInfo> accounts)
    {
        Id = id;
        Name = name;
        Email = email;
        Username = username;
        Accounts = accounts;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public List<AccountInfo> Accounts { get; set; }
}
