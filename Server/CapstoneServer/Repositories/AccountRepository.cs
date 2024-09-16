using MongoDB.Driver;
using System.Threading.Tasks;
using CapstoneServer.Models;
using CapstoneServer.Repositories;
using Microsoft.Extensions.Options;

namespace CapstoneServer.Repositories;
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountRepository(IMongoClient mongoClient, IOptions<MongoDBSettings> mongoSettings)
        {
            var database = mongoClient.GetDatabase(mongoSettings.Value.Database);
            _accounts = database.GetCollection<Account>(mongoSettings.Value.AccountCollection);
        }

        public async Task<Account> GetAccountByIdAsync(string accountId)
        {
            return await _accounts.Find(a => a.Id == accountId).FirstOrDefaultAsync();
        }
        public async Task<Account> GetAccountByAccountNumberAsync(string accountNumber)
        {
            return await _accounts.Find(a => a.AccountNumber == accountNumber).FirstOrDefaultAsync();
        }

        public async Task CreateAccountAsync(Account account)
        {
            await _accounts.InsertOneAsync(account);
        }

        public async Task UpdateAccountAsync(string accountNumber, UpdateDefinition<Account> updateDefinition)
        {
            await _accounts.UpdateOneAsync(a => a.AccountNumber == accountNumber, updateDefinition);
        }
    }
