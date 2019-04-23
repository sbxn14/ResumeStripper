using ResumeStripper.Models.AccountModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Z.EntityFramework.Plus;

namespace ResumeStripper.DAL
{
    public class UserRepository : Repository<User>
    {
        private readonly StripperContext _context;

        public UserRepository(StripperContext context) : base(context)
        {
            _context = context;
        }

        public User GetUserByEmail(string email)
        {
            return DbSet.FirstOrDefault(u => u.Emailaddress == email);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<User> GetAllByCompanyName(string name)
        {
            return DbSet.Where(n => n.UserCompany.Name == name).ToList();
        }

        public List<User> GetAllByCompanyId(int id)
        {
            return DbSet.Where(n => n.UserCompany.Id == id).ToList();
        }
    }
}