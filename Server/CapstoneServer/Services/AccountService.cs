using MongoDB.Driver;
using CapstoneServer.Models;
using CapstoneServer.Repositories;
using CapstoneServer.DTOs;
using Microsoft.AspNetCore.Http.Connections;

namespace CapstoneServer.Services;

public class AccountService
{
    private const long minNumber = 100_000_000_000_000;
    private const long maxNumber = 999_999_999_999_999;
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;

    public AccountService(IAccountRepository accountRepository, IUserRepository userRepository)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
    }
    // Method to generate a unique 15-digit account number
    public async Task<AccountDTO> CreateAccountAsync(string userId, CreateAccountDTO createAccountDTO)
    {
        // Generate a unique account number
        string accountNumber;
        Random random = new Random();
        do
        {
            // Generate random parts and concatenate them
            string part1 = random.Next(10000, 99999).ToString(); // 5-digit part
            string part2 = random.Next(10000, 99999).ToString(); // 5-digit part
            string part3 = random.Next(10000, 99999).ToString(); // 5-digit part

            accountNumber = part1 + part2 + part3; // Concatenate to get a 15-digit number
        } while (await _accountRepository.GetAccountByAccountNumberAsync(accountNumber) != null);

        // Create the new account
        var newAccount = new Account
        {
            AccountNumber = accountNumber,
            UserId = userId,
            Balance = createAccountDTO.Balance >= 100 ? createAccountDTO.Balance : 100,
            AccountType = createAccountDTO.AccountType,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        await _accountRepository.CreateAccountAsync(newAccount);

        // Add account to user
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user != null)
        {
            user.Accounts.Add(new AccountInfo
            {
                AccountNum = newAccount.AccountNumber,
                Balance = newAccount.Balance,
                AccNickName = createAccountDTO.NickName
            });
            await _userRepository.UpdateUserAsync(userId, Builders<User>.Update.Set(u => u.Accounts, user.Accounts));
        }

        return new AccountDTO
        {
            AccountId = newAccount.Id,
            AccountNumber = newAccount.AccountNumber,
            Balance = newAccount.Balance,
            AccountType = newAccount.AccountType,
            CreatedAt = newAccount.CreatedAt
        };
    }

    public async Task<AccountDTO> GetAccountByAccountNumberAsync(string accountNumber)
    {
        var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
        if (account == null || !account.IsActive)
        {
            throw new Exception("Account not found or inactive.");
        }

        return new AccountDTO
        {
            AccountId = account.Id,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            AccountType = account.AccountType,
            CreatedAt = account.CreatedAt
        };
    }

    public async Task UpdateAccountTypeAsync(string accountNumber, UpdateAccountDTO updateAccountDTO)
    {
        var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
        if (account == null || !account.IsActive)
        {
            throw new Exception("Account not found or inactive.");
        }

        var updateDefinition = Builders<Account>.Update
            .Set(a => a.AccountType, updateAccountDTO.AccountType)
            .Set(a => a.LastUpdated, DateTime.UtcNow);

        await _accountRepository.UpdateAccountAsync(accountNumber, updateDefinition);
    }

    public async Task UpdateBalanceAsync(string accountNumber, decimal Amount)
    {
        var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
        if (account == null || !account.IsActive)
        {
            throw new Exception("Account not found or inactive.");
        }

        var newBalance = account.Balance + Amount;
        if (newBalance < 100)
        {
            throw new Exception("Account balance cannot be below the minimum of $100.");
        }

        var updateDefinition = Builders<Account>.Update
            .Set(a => a.Balance, newBalance)
            .Set(a => a.LastUpdated, DateTime.UtcNow);

        await _accountRepository.UpdateAccountAsync(accountNumber, updateDefinition);
    }

    public async Task ActivateAccountAsync(string accountNumber)
    {
        var updateDefinition = Builders<Account>.Update
            .Set(a => a.IsActive, true)
            .Set(a => a.LastUpdated, DateTime.UtcNow);

        await _accountRepository.UpdateAccountAsync(accountNumber, updateDefinition);
    }

    public async Task DeactivateAccountAsync(string accountNumber)
    {
        var updateDefinition = Builders<Account>.Update
            .Set(a => a.IsActive, false)
            .Set(a => a.LastUpdated, DateTime.UtcNow);

        await _accountRepository.UpdateAccountAsync(accountNumber, updateDefinition);
    }
}
