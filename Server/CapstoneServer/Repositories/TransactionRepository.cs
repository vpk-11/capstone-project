using CapstoneServer.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace CapstoneServer.Repositories;
public class TransactionRepository : ITransactionRepository
{
	private readonly IMongoCollection<Transaction> _transactions;

	public TransactionRepository(IMongoClient mongoClient, IOptions<MongoDBSettings> mongoSettings)
    {
        var database = mongoClient.GetDatabase(mongoSettings.Value.Database);
        _transactions = database.GetCollection<Transaction>(mongoSettings.Value.TransactionCollection);
    }
	public async Task CreateTransactionAsync(Transaction transaction)
	{
		await _transactions.InsertOneAsync(transaction);
	}

	public async Task<List<Transaction>> GetTransactionsByAccountNumberAsync(string accountNum)
	{
		return await _transactions
			.Find(t => t.FromAccountNum == accountNum || t.ToAccountNum == accountNum)
			.SortByDescending(t => t.Timestamp)
			.ToListAsync();
	}

	public async Task<List<Transaction>> GetTransactionsByUserIdAsync(List<string> accountNums)
	{
		return await _transactions
			.Find(t => accountNums.Contains(t.FromAccountNum) || accountNums.Contains(t.ToAccountNum))
			.SortByDescending(t => t.Timestamp)
			.ToListAsync();
	}
}
