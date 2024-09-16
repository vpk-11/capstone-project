using System;
using CapstoneServer.DTOs;
using CapstoneServer.Models;
using MongoDB.Driver;

namespace CapstoneServer.Repositories;
public interface IUserRepository
{
    Task<User> GetUserByIdAsync(string id);
    Task<User> GetUserByUsernameAsync(string username);
    Task CreateUserAsync(User user);
    Task DeleteUserAsync(string id);
    Task UpdateUserAsync(string userId, UpdateDefinition<User> updateDefinition);
}
