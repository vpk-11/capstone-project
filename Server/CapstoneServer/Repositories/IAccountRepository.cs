using MongoDB.Driver;
using CapstoneServer.Models;

namespace CapstoneServer.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(string accountId);
        Task<Account> GetAccountByAccountNumberAsync(string accountNumber);
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(string accountId, UpdateDefinition<Account> updateDefinition);
    }
}