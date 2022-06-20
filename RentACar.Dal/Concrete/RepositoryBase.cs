using Microsoft.EntityFrameworkCore;
using RentACar.Dal.Abstract;
using RentACar.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RentACar.Dal.Concrete
{
    public class RepositoryBase<Tentity, TContext> : IRepositoryBase<Tentity> where Tentity : class, IEntity, new() where TContext : DbContext, new()
    {

        public TContext context { get; set; }
        public RepositoryBase()
        {
            context = new TContext();
        }
        public void Add(Tentity entity)
        {

            var addedEntity = context.Add(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();

        }

        public void Delete(Tentity entity)
        {


            var deletedEntity = context.Remove(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();

        }

        public Tentity Get(Expression<Func<Tentity, bool>> filter = null)
        {

            return context.Set<Tentity>().FirstOrDefault(filter);

        }

        public List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter = null)
        {

            return filter == null ? context.Set<Tentity>().ToList() : context.Set<Tentity>().Where(filter).ToList();

        }

        public IQueryable<Tentity> GetAllInclude(Expression<Func<Tentity, bool>> filter = null, params Expression<Func<Tentity, object>>[] include)
        {

            var query = filter == null ? context.Set<Tentity>() : context.Set<Tentity>().Where(filter);
            return include.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        }

        public void Update(Tentity entity)
        {


            var updateContext = context.Update(entity);
            updateContext.State = EntityState.Modified;
            context.SaveChanges();

        }

    }
}
