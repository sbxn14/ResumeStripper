using ResumeStripper.Helpers;
using ResumeStripper.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ResumeStripper.DAL
{
    public class CvRepository : ICvRepository
    {
        public StripperContext Context { get; set; }

        public CvRepository(StripperContext context)
        {
            Context = context;
        }

        public CV GetById(int id)
        {
            return Context.Cvs.Find(id);
        }

        public void Update(CV entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public List<CV> GetAll()
        {
            return Context.Cvs.ToList();
        }

        public void Add(CV entity)
        {
            Context.Cvs.Add(entity);
        }

        public void Delete(CV entity)
        {
            Context.Cvs.Remove(entity);
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