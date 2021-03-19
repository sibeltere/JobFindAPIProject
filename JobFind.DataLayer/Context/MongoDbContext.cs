using JobFind.CoreLayer.Settings;
using JobFind.DataLayer.Configs;
using JobFind.DataLayer.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _db;
        public MongoDbContext(MongoDbSettings config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<User> User => _db.GetCollection<User>("User");

        public IMongoCollection<CV> CV => _db.GetCollection<CV>("CV");
    }
}
