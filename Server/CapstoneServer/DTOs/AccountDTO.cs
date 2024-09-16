using System;

namespace CapstoneServer.DTOs;

public class AccountDTO
{
	public string AccountId { get; set; }
	public string AccountNumber { get; set; }
	public decimal Balance { get; set; }
	public string AccountType { get; set; }
	public DateTime CreatedAt { get; set; }
}
