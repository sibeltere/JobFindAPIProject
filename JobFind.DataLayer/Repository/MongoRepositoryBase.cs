using JobFind.CoreLayer.Settings;
using JobFind.DataLayer.Context;
using JobFind.DataLayer.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JobFind.DataLayer.Repository
{
    public class MongoRepositoryBase<T> : IMongoRepositoryBase<T>
      where T : BaseEntity
    {
        #region Fields
        private MongoDbContext _dbContext;
        public IMongoCollection<T> Collection { get; private set; }
        private readonly IOptions<MongoDbSettings> _dbSettings;
        #endregion

        #region CTOR
        public MongoRepositoryBase(IOptions<MongoDbSettings> dbSettings)
        {
            this._dbSettings = dbSettings; ;
            _dbContext = new MongoDbContext(_dbSettings.Value);
            Collection = _dbContext.GetCollection<T>();
        }
        #endregion

        #region Methods

        public async Task<T> GetFilter(Expression<Func<T, bool>> filter)
        {
            var model = await Collection.Find(filter).FirstOrDefaultAsync();
            return model;
        }

        public async Task<T> Find(object id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id.ToString(), out objectId))
            {
                return null;
            }
            var filterId = Builders<T>.Filter.Eq("_id", objectId);
            var model = await Collection.Find(filterId).FirstOrDefaultAsync();
            return model;
        }

        public async Task<T> Create(T model)
        {
            await Collection.InsertOneAsync(model);
            return model;
        }

        public async Task<T> Update(T model)
        {
            ReplaceOneResult updateResult = await Collection.ReplaceOneAsync(filter: g => g.Id == model.Id, replacement: model);
            return model;
        }

        public async Task<bool> Delete(object Id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(m => m.Id, Id);
            DeleteResult deleteResult = await Collection.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await Collection.Find(filter).ToListAsync();
        }
        #endregion


    }
}
