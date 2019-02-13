using ResumeStripper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResumeStripper.DAL
{
    public class CVRepository
    {
        private StripperContext context;

        public CVRepository(StripperContext _context)
        {
            context = _context;
        }

        public ICollection<CV> GetAll()
        {
            return context.CVS.ToList();
        }

        public CV Get(int id)
        {
            return context.CVS.Find(id);
        }

        public void Update(CV cv)
        {
            context.Entry(cv).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Insert(CV cv)
        {
            context.CVS.Add(cv);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.CVS.Remove(Get(id));
            context.SaveChanges();
        }
    }
}