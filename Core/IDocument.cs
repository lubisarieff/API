using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Core
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
        System.DateTime CreadAt { get; }
    }
}