using CapstoneServer.Models;

namespace CapstoneServer.Repositories;

public interface ITransactionRepository
{
	Task CreateTransactionAsync(Transaction transaction);
	Task<List<Transaction>> GetTransactionsByAccountNumberAsync(string accountNum);
	Task<List<Transaction>> GetTransactionsByUserIdAsync(List<string> accountIds);
}
