using JobFind.DataLayer.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.Context
{
    public interface IMongoDbContext
    {
        IMongoCollection<User> User { get; }
        IMongoCollection<CV> CV { get; }
    }
}
