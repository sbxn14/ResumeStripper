using ResumeStripper.Helpers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ResumeStripper.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public StripperContext Context { get; set; }
        public IDbSet<T> DbSet { get; set; }

        public Repository(StripperContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            ContextHelper.DisposeContext();
            Context.Dispose();
        }
    }
}