using Company.API.Entities;
using Company.API.Repositories.Interfaces;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Company.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyRepository repository, ILogger<CompanyController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyDetails>>> GetCompanies()
        {
            var companys = await _repository.GetCompanies();
            return Ok(companys);
        }

        [HttpGet("{id:length(24)}", Name = "GetCompany")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyDetails>> GetCompanyById(string id)
        {
            var company = await _repository.GetCompany(id);

            if (company == null)
            {
                _logger.LogError($"Company with id: {id}, not found.");
                return NotFound();
            }

            return Ok(company);
        }

        [Route("[action]/{category}", Name = "GetCompanyByCode")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyDetails>>> GetCompanyByCode(string code)
        {
            var companys = await _repository.GetCompanyByCode(code);
            return Ok(companys);
        }

        [Route("[action]/{name}", Name = "GetCompanyByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<CompanyDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyDetails>>> GetCompanyByName(string name)
        {
            var items = await _repository.GetCompanyByName(name);
            if (items == null)
            {
                _logger.LogError($"Companies with name: {name} not found.");
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyDetails>> CreateCompany([FromBody] CompanyDetails company)
        {
            await _repository.CreateCompany(company);

            return CreatedAtRoute("GetCompany", new { id = company.Id }, company);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDetails company)
        {
            return Ok(await _repository.UpdateCompany(company));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteCompany")]        
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCompanyById(string id)
        {
            return Ok(await _repository.DeleteCompany(id));
        }
    }
}
