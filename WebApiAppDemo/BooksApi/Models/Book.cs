using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace BooksApi.Models
{
    public class Book
    {
        [BsonId]//主键
        [BsonRepresentation(BsonType.ObjectId)]//string到objectid
        public string Id { get; set; }

        [BsonElement("Name")]//指定数据库属性名称
        [JsonProperty("Name")]
        public string BookName { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}
