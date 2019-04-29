using ResumeStripper.Models.AccountModels;
using System.Collections.Generic;
using System.Data.Entity;

namespace ResumeStripper.DAL
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByEmail(string email);
        List<User> GetAllByCompanyName(string name);
        List<User> GetAllByCompanyId(int id);
    }
}