using ResumeStripper.Models.AccountModels;
using System.Data.Entity;
using System.Linq;

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

        public void UpdateCompany(Company company)
        {
            Company fullCompany = GetById(company.Id);

            if (fullCompany != null)
            {
                _context.Entry(fullCompany).CurrentValues.SetValues(company);
            }
        }
    }
}