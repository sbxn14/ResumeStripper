using System.Linq;
using ResumeStripper.Models.AccountModels;

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
    }
}