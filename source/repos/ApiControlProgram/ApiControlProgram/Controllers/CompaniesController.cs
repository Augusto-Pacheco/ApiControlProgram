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

        [HttpGet("company/{Name}")]
        [ProducesResponseType(200, Type = typeof(Companies))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyByName(string Name)
        {
            var company = _mapper.Map<Companies>(_companiesInterface.GetCompanyByName(Name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromBody] CompaniesDto companyCreate)
        {
            if (companyCreate == null)
                return BadRequest(ModelState);

            var company = _companiesInterface.GetCompanies()
                .Where(c => c.Name.Trim().ToUpper() == companyCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (company != null)
            {
                ModelState.AddModelError("", "La compañía que intentas ingresar ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyMap = _mapper.Map<Companies>(companyCreate);

            if (!_companiesInterface.CreateCompany(companyMap))
            {
                ModelState.AddModelError("", "Ocurrió un problema mientras se guardaba la información, por favor intente más tarde.");
                return StatusCode(500, ModelState);
            }

            return Ok("Compañía agregada exitosamente.");
        }
    }
}
