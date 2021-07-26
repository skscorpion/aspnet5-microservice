using Company.API.Data.Interfaces;
using Company.API.Entities;
using Company.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.API.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ICompanyContext _context;

        public CompanyRepository(ICompanyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CompanyDetails>> GetCompanies()
        {
            return await _context
                            .Companies
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<CompanyDetails> GetCompany(string id)
        {
            return await _context
                           .Companies
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompanyDetails>> GetCompanyByName(string name)
        {
            FilterDefinition<CompanyDetails> filter = Builders<CompanyDetails>.Filter.ElemMatch(p => p.Name, name);

            return await _context
                            .Companies
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<CompanyDetails>> GetCompanyByCode(string code)
        {
            FilterDefinition<CompanyDetails> filter = Builders<CompanyDetails>.Filter.Eq(p => p.Code, code);

            return await _context
                            .Companies
                            .Find(filter)
                            .ToListAsync();
        }


        public async Task CreateCompany(CompanyDetails company)
        {
            await _context.Companies.InsertOneAsync(company);
        }

        public async Task<bool> UpdateCompany(CompanyDetails company)
        {
            var updateResult = await _context
                                        .Companies
                                        .ReplaceOneAsync(filter: g => g.Id == company.Id, replacement: company);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCompany(string id)
        {
            FilterDefinition<CompanyDetails> filter = Builders<CompanyDetails>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Companies
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
