using ApiControlProgram.Dto;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using ApiControlProgram.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace ApiControlProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectController(IProjectRepository ProjectInterface, IMapper mapper)
        {
            _projectRepository = ProjectInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        public IActionResult GetProject()
        {

            var project = _mapper.Map<List<ProjectDto>>(_projectRepository.GetProjects());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(project);
        }

        [HttpGet("{ProjectId}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]

        public IActionResult GetProject(int ProjectId)
        {
            if (!_projectRepository.ProjectExist(ProjectId))
                return NotFound();

            var project = _mapper.Map<ProjectDto>(_projectRepository.GetProject(ProjectId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(project);
        }

        [HttpGet("name/{Name}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]

        public IActionResult GetProjectByName(string Name)
        {

            var project = _mapper.Map<ProjectDto>(_projectRepository.GetProjectByName(Name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProject([FromBody] ProjectDto projectCreate)
        {
            if (projectCreate == null)
                return BadRequest(ModelState);

            var company = _projectRepository.GetProjects()
                .Where(c => c.Name.Trim().ToUpper() == projectCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (company != null)
            {
                ModelState.AddModelError("", "El proyecto que intentas ingresar ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectMap = _mapper.Map<Project>(projectCreate);

            if (!_projectRepository.CreateProject(projectMap))
            {
                ModelState.AddModelError("", "Ocurrió un problema mientras se guardaba la información, por favor intente más tarde.");
                return StatusCode(500, ModelState);
            }

            return Ok("Proyecto agregado exitosamente.");
        }

        [HttpPatch("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProject(int projectId, [FromBody] ProjectUpdate updateProject)
        {
            try
            {
                if (updateProject == null)
                    return BadRequest(ModelState);

                if (projectId != updateProject.ProjectId)
                    return BadRequest(ModelState);

                if (!_projectRepository.ProjectExist(projectId))
                    return NotFound();

                var projectMap = _mapper.Map<Project>(updateProject);

                if (!_projectRepository.UpdateProject(projectMap))
                {
                    ModelState.AddModelError("", "Hubo un error mientras se actualizaba, por favor intente más tarde");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ocurrió un error en el servidor: {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }


        [HttpPatch("{projectId}/date")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProject(int projectId)
        {
            if (!_projectRepository.ProjectExist(projectId))
                return NotFound();

            var project = _projectRepository.GetProject(projectId);
            project.FinishDate = DateTime.Now;

            if (!_projectRepository.UpdateProject(project))
            {
                ModelState.AddModelError("", "Hubo un error mientras se actualizaba, por favor intente más tarde");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{projectId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteProject(int projectId)
        {
            if (!_projectRepository.ProjectExist(projectId))
                return NotFound();

            var projectDelete = _projectRepository.GetProject(projectId);

            if (!_projectRepository.DeleteProject(projectDelete))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar la compañía {projectDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
