using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ResumeStripper.DAL
{
    public class Repository<T> where T : class
    {
        private readonly StripperContext _context;
        protected DbSet<T> DbSet { get; set; }

        public Repository(StripperContext context)
        {
            this._context = context;
            DbSet = context.Set<T>();
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

        //TODO add UPDATE somehow, maybe has to be in a POJO specific Repository

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}