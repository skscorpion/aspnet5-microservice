using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _client;

        public CompanyService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CompanyModel>> GetCompany()
        {
            var response = await _client.GetAsync("/api/v1/Company");
            return await response.ReadContentAs<List<CompanyModel>>();
        }

        public async Task<CompanyModel> GetCompany(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Company/{id}");
            return await response.ReadContentAs<CompanyModel>();
        }

        public async Task<IEnumerable<CompanyModel>> GetCompanyByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/Company/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<CompanyModel>>();
        }              
    }
}
