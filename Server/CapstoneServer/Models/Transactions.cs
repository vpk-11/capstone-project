using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CapstoneServer.Models
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("fromAccountNum")]
        public string FromAccountNum { get; set; }

        [BsonElement("toAccountNum")]
        public string ToAccountNum { get; set; }

        [BsonElement("amount")]
        public decimal Amount { get; set; }

        [BsonElement("transactionType")]
        public string TransactionType { get; set; } // Nullable

        [BsonElement("description")]
        public string Description { get; set; } // Nullable

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
