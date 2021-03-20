using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.Entities
{
    public interface IEntity
    {
        [BsonId]
        ObjectId Id { get; set; }
        DateTime CreateDateTime { get; }
    }
}
