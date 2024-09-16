using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CapstoneServer.Models;

public class User
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	[BsonElement("name")]
	public string Name { get; set; }

	[BsonElement("email")]
	public string Email { get; set; }

	[BsonElement("username")]
	public string Username { get; set; }

	[BsonElement("passwordHash")]
	public string PasswordHash { get; set; }

	[BsonElement("accounts")]
	public List<AccountInfo> Accounts { get; set; }

	[BsonElement("createdAt")]
	public DateTime CreatedAt { get; set; }
	
	[BsonElement("lastUpdated")]
    public DateTime LastUpdated { get; set; }
}

public class AccountInfo
{
	public string AccountNum { get; set; }
	public decimal Balance { get; set; }
	public string AccNickName { get; set; }
}