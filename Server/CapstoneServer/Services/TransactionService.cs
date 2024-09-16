using CapstoneServer.DTOs;
using CapstoneServer.Models;
using CapstoneServer.Repositories;
using MongoDB.Driver;

namespace CapstoneServer.Services;
public class TransactionService
{
	private readonly ITransactionRepository _transactionRepository;
	private readonly IAccountRepository _accountRepository;
	private readonly IUserRepository _userRepository;

	public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUserRepository userRepository)
	{
		_transactionRepository = transactionRepository;
		_accountRepository = accountRepository;
		_userRepository = userRepository;
	}

	public async Task CreateTransactionAsync(CreateTransactionDTO dto)
	{
		var fromAccount = await _accountRepository.GetAccountByAccountNumberAsync(dto.FromAccountNum);
		var toAccount = await _accountRepository.GetAccountByAccountNumberAsync(dto.ToAccountNum);

		if (fromAccount == null || toAccount == null)
			throw new Exception("One or both accounts not found.");
		if (fromAccount.Balance < dto.Amount)
			throw new Exception("Insufficient funds.");

		// Deduct from the FromAccount
		var fromUpdate = Builders<Account>.Update
			.Set(a => a.Balance, fromAccount.Balance - dto.Amount);
		await _accountRepository.UpdateAccountAsync(fromAccount.AccountNumber, fromUpdate);

		// Add to the ToAccount
		var toUpdate = Builders<Account>.Update
			.Set(a => a.Balance, toAccount.Balance + dto.Amount);
		await _accountRepository.UpdateAccountAsync(toAccount.AccountNumber, toUpdate);

		// Create the transaction record
		var transaction = new Transaction
		{
			FromAccountNum = dto.FromAccountNum,
			ToAccountNum = dto.ToAccountNum,
			Amount = dto.Amount,
			TransactionType = dto.TransactionType,
			Description = dto.Description,
			Timestamp = DateTime.UtcNow
		};
		await _transactionRepository.CreateTransactionAsync(transaction);
	}

	public async Task<List<TransactionDTO>> GetTransactionsByAccountNumberAsync(string accountNum)
	{
		var transactions = await _transactionRepository.GetTransactionsByAccountNumberAsync(accountNum);
		return transactions.Select(t => new TransactionDTO
		{
			Id = t.Id,
			FromAccountNum = t.FromAccountNum,
			ToAccountNum = t.ToAccountNum,
			Amount = t.Amount,
			TransactionType = t.TransactionType,
			Description = t.Description,
			Timestamp = t.Timestamp
		}).ToList();
	}

	public async Task<List<TransactionDTO>> GetTransactionsByUserIdAsync(string userId)
	{
		var user = await _userRepository.GetUserByIdAsync(userId);
		var accountNums = user.Accounts.Select(a => a.AccountNum).ToList();
		var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(accountNums);
		return transactions.Select(t => new TransactionDTO
		{
			Id = t.Id,
			FromAccountNum = t.FromAccountNum,
			ToAccountNum = t.ToAccountNum,
			Amount = t.Amount,
			TransactionType = t.TransactionType,
			Description = t.Description,
			Timestamp = t.Timestamp
		}).ToList();
	}
}
