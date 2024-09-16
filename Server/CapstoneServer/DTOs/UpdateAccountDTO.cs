namespace CapstoneServer.DTOs;
public class UpdateAccountDTO
{
	public string? AccountType { get; set; }
	public decimal? Amount { get; set; } // Nullable to allow partial updates
}