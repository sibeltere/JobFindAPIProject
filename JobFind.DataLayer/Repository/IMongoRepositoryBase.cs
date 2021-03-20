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
        object Create(T model);
    }
}
