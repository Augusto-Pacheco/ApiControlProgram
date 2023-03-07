using ApiControlProgram.Dto;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using ApiControlProgram.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ApiControlProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IMapper _mapper;
        public CompaniesController(ICompaniesRepository companiesInterface, IMapper mapper)
        {
            _companiesRepository = companiesInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Companies>))]
        public IActionResult GetCompanies()
        {

            var companies = _mapper.Map<List<CompaniesDto>>(_companiesRepository.GetCompanies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(companies);
        }

        [HttpGet("{CompanyId}")]
        [ProducesResponseType(200, Type = typeof(Companies))]
        [ProducesResponseType(400)]
        public IActionResult GetCompany(int CompanyId)
        {
            if (!_companiesRepository.CompanyExist(CompanyId))
                return NotFound();

            var company = _mapper.Map<CompaniesDto>(_companiesRepository.GetCompany(CompanyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [HttpGet("company/{Name}")]
        [ProducesResponseType(200, Type = typeof(Companies))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyByName(string Name)
        {
            var company = _mapper.Map<CompaniesDto>(_companiesRepository.GetCompanyByName(Name));

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

            var company = _companiesRepository.GetCompanies()
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

            if (!_companiesRepository.CreateCompany(companyMap))
            {
                ModelState.AddModelError("", "Ocurrió un problema mientras se guardaba la información, por favor intente más tarde.");
                return StatusCode(500, ModelState);
            }

            return Ok("Compañía agregada exitosamente.");
        }

        [HttpPatch("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompany(int companyId, [FromBody] CompaniesDto updateCompany)
        {
            if (updateCompany == null)
                return BadRequest(ModelState);

            if (companyId != updateCompany.CompanyId)
                return BadRequest(ModelState);

            if (!_companiesRepository.CompanyExist(companyId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyMap = _mapper.Map<Companies>(updateCompany);

            if (!_companiesRepository.UpdateCompany(companyMap))
            {
                ModelState.AddModelError("", "Hubo un error mientras se actualizaba, por favor intente más tarde");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{companyId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCompany(int companyId)
        {
            if (!_companiesRepository.CompanyExist(companyId))
                return NotFound();

            var companyToDelete = _companiesRepository.GetCompany(companyId);

            if (!_companiesRepository.DeleteCompanies(companyToDelete))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar la compañía {companyToDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
