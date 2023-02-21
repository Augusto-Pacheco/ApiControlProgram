using ApiControlProgram.Dto;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace ApiControlProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesRepository _companiesInterface;
        private readonly IMapper _mapper;
        public CompaniesController(ICompaniesRepository companiesInterface, IMapper mapper)
        {
            _companiesInterface = companiesInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Companies>))]
        public IActionResult GetCompanies()
        {

            var companies = _mapper.Map<List<CompaniesDto>>(_companiesInterface.GetCompanies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(companies);
        }

        [HttpGet("{CompanyId}")]
        [ProducesResponseType(200, Type = typeof(Companies))]
        [ProducesResponseType(400)]
        public IActionResult GetCompany(int CompanyId)
        {
            if (!_companiesInterface.CompanyExist(CompanyId))
                return NotFound();

            var company = _mapper.Map<Companies>(_companiesInterface.GetCompany(CompanyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

    }
}
