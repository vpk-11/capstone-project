namespace CapstoneServer.DTOs;
public class TransactionDTO
{
	public string Id { get; set; }
	public string FromAccountNum { get; set; }
	public string ToAccountNum { get; set; }
	public decimal Amount { get; set; }
	public string TransactionType { get; set; }
	public string Description { get; set; }
	public DateTime Timestamp { get; set; }
}
