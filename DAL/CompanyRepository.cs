using System.Collections.Generic;
using ResumeStripper.Models.AccountModels;
using System.Data.Entity;
using System.Linq;

namespace ResumeStripper.DAL
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly StripperContext _context;

        public CompanyRepository(StripperContext context)
        {
            _context = context;
            DbSet = _context.Set<Company>();
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

        public IDbSet<Company> DbSet { get; set; }
        public Company GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Company> GetAll()
        {
            return _context.Companies.ToList();
        }

        public Company Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Company entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Company entity)
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}