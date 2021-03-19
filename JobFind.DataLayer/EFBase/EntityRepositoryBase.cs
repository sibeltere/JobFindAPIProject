using JobFind.DataLayer.Context;
using JobFind.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JobFind.DataLayer.EFBase
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity>
         where TEntity : class, IEntity, new()
         where TContext : MongoDbContext, new()
    {
        public bool Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                return context.
            }
        }

        public IList<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
