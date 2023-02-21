using ApiControlProgram.Dto;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiControlProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {

        private readonly IProjectRepository _ProjectInterface;
        private readonly IMapper _mapper;
        public ProjectController(IProjectRepository ProjectInterface, IMapper mapper)
        {
            _ProjectInterface = ProjectInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        public IActionResult GetProject()
        {

            var project = _mapper.Map<List<ProjectDto>>(_ProjectInterface.GetProjects());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(project);
        }

        [HttpGet("{ProjectId}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]

        public IActionResult GetProject(int id)
        {
            if (!_ProjectInterface.ProjectExist(id))
                return NotFound();

            var project = _mapper.Map<Project>(_ProjectInterface.GetProject(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(project);
        }
    }
}
