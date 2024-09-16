using System;

namespace CapstoneServer.Models;

public class MongoDBSettings
{
	public string Database { get; set; }
	public string UserCollection { get; set; }
	public string AccountCollection { get; set; }
	public string TransactionCollection { get; set; }
}
