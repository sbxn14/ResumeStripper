using System.Collections.Generic;
using System.Data.Entity;
using ResumeStripper.Models.AccountModels;

namespace ResumeStripper.DAL
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company GetByName(string companyName);
    }
}