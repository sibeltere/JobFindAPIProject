using JobFind.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JobFind.DataLayer.EFBase
{
    public interface IEntityRepositoryBase<T> where T : class, IEntity, new()
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IList<T>
        SearchFor(Expression<Func<T, bool>> predicate);
        IList<T> GetAll();
        T GetById(Guid id);
    }
}
