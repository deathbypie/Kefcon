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
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }

    public abstract class ServiceBase<T> : IServiceBase<T> where T : BaseEntity
    {
        protected ApplicationDataContext _context;
        private DbSet<T> entities;

        public ServiceBase(ApplicationDataContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = entities.SingleOrDefault(s => s.Id == id);
            Delete(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
