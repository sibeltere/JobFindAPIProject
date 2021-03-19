using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.Entities
{
    public class BaseEntity : IEntity
    {
        public ObjectId Id { get; set; }

        public DateTime CreateDateTime => Id.CreationTime;
    }
}
