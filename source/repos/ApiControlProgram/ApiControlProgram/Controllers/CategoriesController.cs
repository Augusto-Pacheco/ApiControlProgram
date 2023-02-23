using ApiControlProgram.Dto;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiControlProgram.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ExtrasController : Controller
    {
        private readonly IExtrasRepository _extrasRepository;
        private readonly IMapper _mapper;


        public ExtrasController(IExtrasRepository extrasRepository, IMapper mapper)
        {
            _extrasRepository = extrasRepository;
            _mapper = mapper;
        }

        [HttpGet("categories")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Categories>))]

        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoriesDto>>(_extrasRepository.GetCategories());

            if(!ModelState.IsValid)
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("types")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Types>))]

        public IActionResult GetTypes()
        {
            var types = _mapper.Map<List<TypesDto>>(_extrasRepository.GetTypes());

            if (!ModelState.IsValid)
                return NotFound();

            return Ok(types);
        }
    }
}
