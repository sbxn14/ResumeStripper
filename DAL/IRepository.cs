using System.Collections.Generic;
using System.Data.Entity;

namespace ResumeStripper.DAL
{
    public interface IRepository<T> where T : class
    {
        StripperContext Context { get; set; }
        T GetById(int id);
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
        void Dispose();
    }
}