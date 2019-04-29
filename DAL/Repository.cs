using ResumeStripper.Helpers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ResumeStripper.Models.AccountModels;

namespace ResumeStripper.DAL
{
    public class Repository<T> where T : class
    {
        public virtual StripperContext _context { get; set; }
        protected virtual DbSet<T> DbSet { get; set; }

        public Repository(StripperContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        //public Repository(StripperContext context, DbSet<T> set)
        //{
        //    //for testing
        //    _context = context;
        //    DbSet = (DbSet<T) set;
        //}

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
            ContextHelper.DisposeContext();
            _context.Dispose();
        }
    }
}