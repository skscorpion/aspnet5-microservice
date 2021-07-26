using Company.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.API.Data
{
    public class CompanyContextSeed
    {
        public static void SeedData(IMongoCollection<CompanyDetails> companies)
        {
            bool existCompany = companies.Find(p => true).Any();
            if (!existCompany)
            {
                companies.InsertManyAsync(GetPreconfiguredCompanies());
            }
        }

        private static IEnumerable<CompanyDetails> GetPreconfiguredCompanies()
        {
            return new List<CompanyDetails>()
            {
                new CompanyDetails()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Code = "ril",
                    Name = "Reliance Industries",
                    CEO = "Mukesh Ambani",
                    TurnOver = "5400000000",
                    Website = "www.ril.com",
                    StockExchange = "NSE",
                    IsActive = true,
                    CreatedBy = 12345,
                    CreatedOn = DateTime.Now
                },
                new CompanyDetails()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Code = "infy",
                    Name = "Infosys Limited",
                    CEO = "test ceo",
                    TurnOver = "950000000",
                    Website = "www.infosys.com",
                    StockExchange = "NSE",
                    IsActive = true,
                    CreatedBy = 12345,
                    CreatedOn = DateTime.Now
                },
                new CompanyDetails()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Code = "mrf",
                    Name = "MRF Tyres",
                    CEO = "test ceo",
                    TurnOver = "680000000",
                    Website = "www.mrf.com",
                    StockExchange = "NSE",
                    IsActive = true,
                    CreatedBy = 12345,
                    CreatedOn = DateTime.Now
                }
            };
        }
    }
}
