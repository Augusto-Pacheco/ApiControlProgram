using ApiControlProgram.Dto;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiControlProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {

        private readonly ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;

        public TasksController(ITasksRepository tasksRepository, IMapper mapper)
        {
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Tasks))]

        public IActionResult GetTasks()
        {
            var task = _mapper.Map<List<TasksDto>>(_tasksRepository.GetTasks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpGet("{TaskId}")]
        [ProducesResponseType(200, Type = typeof(Tasks))]
        [ProducesResponseType(400)]

        public IActionResult GetTask(int TaskId)
        {
            if (!_tasksRepository.TaskExist(TaskId))
                return NotFound();

            var task = _mapper.Map<TasksDto>(_tasksRepository.GetTask(TaskId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpGet("noRegis/{NoRegis}")]
        [ProducesResponseType(200, Type = typeof(Tasks))]
        [ProducesResponseType(400)]

        public IActionResult GetTaskRegis(string NoRegis)
        {

            var task = _mapper.Map<TasksDto>(_tasksRepository.GetTaskRegis(NoRegis));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTasks([FromBody] TasksDto taskCreate)
        {
            if (taskCreate == null)
                return BadRequest(ModelState);

            var company = _tasksRepository.GetTasks()
                .Where(c => c.Name.Trim().ToUpper() == taskCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (company != null)
            {
                ModelState.AddModelError("", "La tarea que intentas ingresar ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskMap = _mapper.Map<Tasks>(taskCreate);

            if (!_tasksRepository.CreateTask(taskMap))
            {
                ModelState.AddModelError("", "Ocurrió un problema mientras se guardaba la información, por favor intente más tarde.");
                return StatusCode(500, ModelState);
            }

            return Ok("Tarea agregada exitosamente.");
        }
    }
}
