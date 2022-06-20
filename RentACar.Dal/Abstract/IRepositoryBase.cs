using Microsoft.EntityFrameworkCore;
using RentACar.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RentACar.Dal.Abstract
{
    public interface IRepositoryBase<Tentity> where Tentity : class,IEntity, new()
    {
        void Add(Tentity entity);
        void Delete(Tentity entity);
        void Update(Tentity entity);
        Tentity Get(Expression<Func<Tentity, bool>> filter = null);
        List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter = null);
        IQueryable<Tentity> GetAllInclude(Expression<Func<Tentity, bool>> filter = null, params Expression<Func<Tentity, object>>[] include);
    }
}
