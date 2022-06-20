using RentACar.BL.Abstract;
using RentACar.Dal.Abstract;
using RentACar.Dal.Concrete;
using RentACar.Dal.Contexts;
using RentACar.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RentACar.BL.Concrete
{
    public class ManagerBase<T> : IServiceBase<T> where T : class, IEntity, new()
    {
        private readonly IRepositoryBase<T> repositoryBase;
        public ManagerBase()
        {
            repositoryBase = new RepositoryBase<T, AppIdentityDbContext>();
        }
        public void Add(T entity)
        {
            repositoryBase.Add(entity);
        }

        public void Delete(T entity)
        {
            repositoryBase.Delete(entity);
        }

        public T Get(Expression<Func<T, bool>> filter = null)
        {
            return repositoryBase.Get(filter);
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return repositoryBase.GetAll(filter);
        }

        public IQueryable<T> GetAllInclude(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] include)
        {
            return repositoryBase.GetAllInclude(filter, include);
        }

        public void Update(T entity)
        {
            repositoryBase.Update(entity);
        }
    }
}
