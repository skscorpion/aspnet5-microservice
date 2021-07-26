using Company.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.API.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyDetails>> GetCompanies();
        Task<CompanyDetails> GetCompany(string id);
        Task<IEnumerable<CompanyDetails>> GetCompanyByName(string name);
        Task<IEnumerable<CompanyDetails>> GetCompanyByCode(string code);

        Task CreateCompany(CompanyDetails company);
        Task<bool> UpdateCompany(CompanyDetails company);
        Task<bool> DeleteCompany(string id);
    }
}
