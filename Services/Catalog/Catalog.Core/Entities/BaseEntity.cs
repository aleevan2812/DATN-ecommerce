using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class BaseEntity
{
    [BsonId]
    public string Id { get; set; }
}