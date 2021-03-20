using JobFind.CoreLayer.Settings;
using JobFind.DataLayer.Configs;
using JobFind.DataLayer.Context;
using JobFind.DataLayer.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.DataLayer.Repository
{
    public class MongoRepositoryBase<T> : IMongoRepositoryBase<T>
      where T : BaseEntity
    {

        private MongoDbContext _dbContext;
        public IMongoCollection<T> Collection { get; private set; }
        private readonly IOptions<MongoDbSettings> _dbSettings;


        public MongoRepositoryBase(IOptions<MongoDbSettings> dbSettings)
        {
            this._dbSettings = dbSettings; ;
            _dbContext = new MongoDbContext(_dbSettings.Value);
            Collection = _dbContext.DbSet<T>();
        }

        public object Create(T model)
        {
            try
            {
                Collection.InsertOne(model);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
