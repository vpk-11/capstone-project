using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CapstoneServer.Models;

public class Account
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	[BsonElement("userId")]
	public required string UserId { get; set; }

	[BsonElement("accountNumber")]
	public string AccountNumber { get; set; }

	[BsonElement("accountType")]
	public string AccountType { get; set; }

	[BsonElement("balance")]
	public decimal Balance { get; set; }

	[BsonElement("createdAt")]
	public DateTime CreatedAt { get; set; }
	
	[BsonElement("lastUpdated")]
	public DateTime LastUpdated { get; set; }

	[BsonElement("isActive")]
	public bool IsActive { get; set; }
}