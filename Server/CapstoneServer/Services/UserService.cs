using CapstoneServer.DTOs;
using CapstoneServer.Models;
using CapstoneServer.Repositories;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using MongoDB.Driver;

namespace CapstoneServer.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public UserService(IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        return user;
    }

    public async Task CreateUserAsync(UserRegistrationDTO registrationDTO)
    {
        // Check if username already exists
        var existingUser = await _userRepository.GetUserByUsernameAsync(registrationDTO.Username);
        if (existingUser != null)
        {
            throw new Exception("Username already exists.");
        }

        var newUser = new User
        {
            Name = registrationDTO.Name,
            Email = registrationDTO.Email,
            Username = registrationDTO.Username,
            PasswordHash = registrationDTO.Password, // Store password in plaintext for now
            CreatedAt = DateTime.UtcNow,
            Accounts = new List<AccountInfo>()
        };

        await _userRepository.CreateUserAsync(newUser);
    }
    public async Task<User> LoginUser(UserLoginDTO loginDto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        if (user.PasswordHash != loginDto.Password)
        {
            throw new Exception("Password is incorrect.");
        }

        return user;
    }
    public async Task<User> UpdateUserAsync(string userId, UserUpdateDTO updateDTO)
{
    // Fetch the user from the repository
    var user = await _userRepository.GetUserByIdAsync(userId);
    if (user == null)
    {
        throw new Exception($"User not found.");
    }

    // Build the update definition
    var updateDefinitions = new List<UpdateDefinition<User>>();

    if (!string.IsNullOrEmpty(updateDTO.Name))
    {
        updateDefinitions.Add(Builders<User>.Update.Set(u => u.Name, updateDTO.Name));
    }

    if (!string.IsNullOrEmpty(updateDTO.Email))
    {
        updateDefinitions.Add(Builders<User>.Update.Set(u => u.Email, updateDTO.Email));
    }

    if (!string.IsNullOrEmpty(updateDTO.Username))
    {
        // Ensure the username is unique
        var existingUser = await _userRepository.GetUserByUsernameAsync(updateDTO.Username);
        if (existingUser != null && existingUser.Id != userId)
        {
            throw new Exception("Username is already taken.");
        }
        updateDefinitions.Add(Builders<User>.Update.Set(u => u.Username, updateDTO.Username));
    }

    updateDefinitions.Add(Builders<User>.Update.Set(u => u.LastUpdated, DateTime.UtcNow));

    // Combine all update definitions
    var updateDefinition = Builders<User>.Update.Combine(updateDefinitions);

    // Perform the update operation
    await _userRepository.UpdateUserAsync(userId, updateDefinition);

    // Fetch and return the updated user
    var updatedUser = await _userRepository.GetUserByIdAsync(userId);
    return updatedUser;
}


    public async Task UpdatePasswordAsync(string userId, UpdatePasswordDTO passwordDTO)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        if (user.PasswordHash != passwordDTO.OldPassword)
        {
            throw new Exception("Old password is incorrect.");
        }

        var updateDefinition = Builders<User>.Update
            .Set(u => u.PasswordHash, passwordDTO.NewPassword)
            .Set(u => u.LastUpdated, DateTime.UtcNow);

        await _userRepository.UpdateUserAsync(userId, updateDefinition);
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        // Add logic to deactivate all associated accounts instead of deleting
        // foreach (var account in user.Accounts)
        // {
        //     account.IsActive = false; // Assuming the AccountInfo class has IsActive field
        // }

        var updateDefinition = Builders<User>.Update.Set(u => u.Accounts, user.Accounts);
        await _userRepository.UpdateUserAsync(userId, updateDefinition);

        // Now, delete the user
        await _userRepository.DeleteUserAsync(userId);
    }
}

