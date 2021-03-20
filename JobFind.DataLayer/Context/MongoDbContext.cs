using JobFind.CoreLayer.Settings;
using JobFind.DataLayer.Configs;
using JobFind.DataLayer.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace JobFind.DataLayer.Context
{
    public class MongoDbContext:IMongoDbContext
    {
        private MongoClient _mongodbClient;
        private readonly IMongoDatabase _db;
        public MongoDbContext(MongoDbSettings config)
        {
            _mongodbClient = new MongoClient(config.ConnectionString);
            _db = _mongodbClient.GetDatabase(config.Database);
        }

        public IMongoCollection<T> DbSet<T>() where T : BaseEntity
        {
            var table = typeof(T).GetCustomAttribute<TableAttribute>(false).Name;
            return _db.GetCollection<T>(table);

        }


    }
}
