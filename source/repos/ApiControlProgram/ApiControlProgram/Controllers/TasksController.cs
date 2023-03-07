using ApiControlProgram.Dto;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using ApiControlProgram.Repositories;
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

        [HttpPatch("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int taskId, [FromBody] TasksDto updateTask)
        {
            if (updateTask == null)
                return BadRequest(ModelState);

            if (taskId != updateTask.TaskId)
                return BadRequest(ModelState);

            if (!_tasksRepository.TaskExist(taskId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var taskMap = _mapper.Map<Tasks>(updateTask);

            if (!_tasksRepository.UpdateTask(taskMap))
            {
                ModelState.AddModelError("", "Hubo un error mientras se actualizaba, por favor intente más tarde");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{taskId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTask(int taskId)
        {
            if (!_tasksRepository.TaskExist(taskId))
                return NotFound();

            var TaskDelete = _tasksRepository.GetTask(taskId);

            if (!_tasksRepository.DeleteTask(TaskDelete))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar la compañía {TaskDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
