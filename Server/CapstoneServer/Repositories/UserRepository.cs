using System;
using CapstoneServer.DTOs;
using CapstoneServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CapstoneServer.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoClient mongoClient, IOptions<MongoDBSettings> mongoSettings)
    {
        var database = mongoClient.GetDatabase(mongoSettings.Value.Database);
        _users = database.GetCollection<User>(mongoSettings.Value.UserCollection);
    }

    // Basic CRUD operations

    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateUserAsync(string userId, UpdateDefinition<User> updateDefinition)
    {
        if (updateDefinition != null)
        {
            await _users.UpdateOneAsync(u => u.Id == userId, updateDefinition);
        }
    }

    public async Task DeleteUserAsync(string userId)
    {
        await _users.DeleteOneAsync(u => u.Id == userId);
    }
}
