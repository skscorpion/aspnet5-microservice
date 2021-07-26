using Shopping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyModel>> GetCompany();
        Task<IEnumerable<CompanyModel>> GetCompanyByCategory(string category);
        Task<CompanyModel> GetCompany(string id);        
    }
}
