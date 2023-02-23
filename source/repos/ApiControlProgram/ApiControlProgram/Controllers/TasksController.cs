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
    }
}
