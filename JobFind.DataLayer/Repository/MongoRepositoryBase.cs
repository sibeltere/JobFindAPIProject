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

        public async Task Create(T model)
        {
            await Collection.InsertOneAsync(model);
        }

        public async Task<bool> Update(T model)
        {
            ReplaceOneResult updateResult = await Collection.ReplaceOneAsync(filter: g => g.Id == model.Id, replacement: model);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(object Id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(Id.ToString(), out objectId))
            {
                return false;
            }
            var filterId = Builders<T>.Filter.Eq("_id", objectId);
            var deleted = await Collection.FindOneAndDeleteAsync(filterId);
            return deleted != null;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await Collection.Find(x => true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await Collection.Find(filter).ToListAsync();
        }
        #endregion


    }
}
