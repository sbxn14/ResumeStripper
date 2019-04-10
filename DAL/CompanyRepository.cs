using System.Linq;
using ResumeStripper.Models.AccountModels;

namespace ResumeStripper.DAL
{
    public class CompanyRepository : Repository<Company>
    {
        private readonly StripperContext _context;

        public CompanyRepository(StripperContext context) : base(context)
        {
            _context = context;
        }

        public Company GetByName(string companyName)
        {
            return DbSet.FirstOrDefault(c => c.Name == companyName);
        }
    }
}