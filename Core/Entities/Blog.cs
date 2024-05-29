using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlogApi.Core.Entities
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("text")]
        public string Text { get; set; } = string.Empty;

        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; }

        [BsonElement("modifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [BsonElement("deletedOn")]
        public DateTime? DeletedOn { get; set; }

        [BsonElement("relatedBlogs")]
        public List<string> RelatedBlogs { get; set; } = new List<string>();
    }
}
