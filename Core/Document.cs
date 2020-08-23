using System;
using MongoDB.Bson;
namespace API.Core
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreateAt => Id.CreationTime;
    }
}