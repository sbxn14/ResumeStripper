using ResumeStripper.Helpers;
using ResumeStripper.Models.AccountModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Z.EntityFramework.Plus;

namespace ResumeStripper.DAL
{
    public class UserRepository : IUserRepository
    {
        public StripperContext Context { get; set; }

        public UserRepository(StripperContext context)
        {
            Context = context;
        }

        public User GetUserByEmail(string email)
        {
            return Context.Users.FirstOrDefault(u => u.Emailaddress == email);
        }

        public List<User> GetAllByCompanyName(string name)
        {
            return Context.Users.Where(n => n.UserCompany.Name == name).ToList();
        }

        public List<User> GetAllByCompanyId(int id)
        {
            return Context.Users.Where(n => n.UserCompany.Id == id).ToList();
        }

        public User GetById(int id)
        {
            return Context.Users.Find(id);
        }

        public void Update(User entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public List<User> GetAll()
        {
            return Context.Users.ToList();
        }

        public void Add(User entity)
        {
            Context.Users.Add(entity);
        }

        public void Delete(User entity)
        {
            Context.Users.Remove(entity);
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