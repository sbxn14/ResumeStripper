using ResumeStripper.Helpers;
using ResumeStripper.Models.AccountModels;
using System.Collections.Generic;
using System.Linq;

namespace ResumeStripper.DAL
{
    public class CompanyRepository : ICompanyRepository
    {
        public StripperContext Context { get; set; }

        public CompanyRepository(StripperContext context)
        {
            Context = context;
        }

        public Company GetByName(string companyName)
        {
            return Context.Companies.FirstOrDefault(c => c.Name == companyName);
        }

        public void Update(Company entity)
        {
            Company fullCompany = GetById(entity.Id);

            if (fullCompany != null)
            {
                Context.Entry(entity).CurrentValues.SetValues(entity);
            }
        }

        public Company GetById(int id)
        {
            return Context.Companies.Find(id);
        }

        public List<Company> GetAll()
        {
            return Context.Companies.ToList();
        }

        public void Add(Company entity)
        {
            Context.Companies.Add(entity);
        }

        public void Delete(Company entity)
        {
            Context.Companies.Remove(entity);
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