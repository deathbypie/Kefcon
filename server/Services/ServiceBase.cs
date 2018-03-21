using Kefcon.Data;
using Kefcon.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Services
{
    public interface IServiceBase<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(T entity);
        T Update(T entity);
        void Delete(Guid id);
    }

    public abstract class ServiceBase<T> : IServiceBase<T> where T : BaseEntity
    {
        protected ApplicationDataContext _context;
        protected DbSet<T> entities;

        public ServiceBase(ApplicationDataContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public virtual T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if(entity.Id == null || entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entities.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            var entity = entities.SingleOrDefault(s => s.Id == id);
            Delete(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public virtual T GetById(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
    }
}
