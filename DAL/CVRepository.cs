using ResumeStripper.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ResumeStripper.DAL
{
    public class CvRepository
    {
        private readonly StripperContext _context;

        public CvRepository(StripperContext context)
        {
            this._context = context;
        }

        public ICollection<CV> GetAll()
        {
            return _context.Cvs.ToList();
        }

        public CV Get(int id)
        {
            return _context.Cvs.Find(id);
        }

        public void Update(CV cv)
        {
            _context.Entry(cv).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(CV cv)
        {
            _context.Cvs.Add(cv);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Cvs.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}