using System.Collections.Generic;
using System.Data.Entity;

namespace ResumeStripper.DAL
{
    public interface IRepository<T> where T : class
    {
        IDbSet<T> DbSet { get; set; }
        T GetById(int id);
        List<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Delete(T entity);
        void SaveChanges();
        void Dispose();
    }
}