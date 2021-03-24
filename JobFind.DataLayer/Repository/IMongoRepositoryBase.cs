using JobFind.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.DataLayer.Repository
{
    public interface IMongoRepositoryBase<T> where T : IEntity
    {
        Task<T> GetFilter(Expression<Func<T, bool>> filter);
        Task<T> Find(object id);
        Task<T> Create(T model);
        Task<T> Update(T model);
        Task<bool> Delete(object id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter);

    }
}
