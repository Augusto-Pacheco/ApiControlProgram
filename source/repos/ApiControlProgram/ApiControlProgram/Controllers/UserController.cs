using ApiControlProgram.Dto;
using ApiControlProgram.Helper;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using ApiControlProgram.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ApiControlProgram.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        //private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            //_userManager = userManager; // Se inyecta el servicio de UserManager en el constructor
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            // Generar Id aleatorio
            var userId = Guid.NewGuid().ToString();

            // Generar UserName a partir del nombre y apellido del usuario
            var userName = (userCreate.FirstName.Trim() + userCreate.LastName.Trim())
                .ToLower()
                .Replace(" ", "");

            // Normalizar UserName y Email
            userName = userName.RemoveAccents();
            var normalizedUserName = userName.Normalize();
            var normalizedEmail = userCreate.Email.Normalize();

            // Generar Hash de contraseña
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var passwordHash = passwordHasher.HashPassword(null, userCreate.Password);

            // Mapear DTO a ApplicationUser
            var userMap = new ApplicationUser
            {
                Id = userId,
                UserName = userName,
                NormalizedUserName = normalizedUserName,
                Email = userCreate.Email,
                NormalizedEmail = normalizedEmail,
                FirstName = userCreate.FirstName,
                LastName = userCreate.LastName,
                PhoneNumber = userCreate.PhoneNumber,
                PasswordHash = passwordHash
            };

            // Agregar usuario a la base de datos
            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Ocurrió un problema mientras se guardaba la información, por favor intente más tarde.");
                return StatusCode(500, ModelState);
            }

            var userDto = _mapper.Map<UserDto>(userMap);
            return Ok(userDto);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ApplicationUser>))]
        public IActionResult GetCompanies()
        {

            var user = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("name/{Name}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ApplicationUser>))]
        [ProducesResponseType(400)]
        public IActionResult GetByName(string Name)
        {
            if (!_userRepository.NameExist(Name))
                return NotFound();

            var company = _mapper.Map<List<UserDto>>(_userRepository.GetUserByName(Name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [HttpGet("{UserName}")]
        [ProducesResponseType(200, Type = typeof(ApplicationUser))]
        [ProducesResponseType(400)]
        public IActionResult GetByUserName(string UserName)
        {
            if (!_userRepository.UserExist(UserName))
                return NotFound();

            var company = _mapper.Map<UserDto>(_userRepository.GetUser(UserName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [HttpPatch("{UserName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompany(string UserName, [FromBody] UserDto updateUser)
        {
            if (updateUser == null)
                return BadRequest(ModelState);

            if (UserName != updateUser.UserName)
                return BadRequest(ModelState);

            if (!_userRepository.UserExist(UserName))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<ApplicationUser>(updateUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Hubo un error mientras se actualizaba, por favor intente más tarde");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("delete/{UserName}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCompany(string UserName)
        {
            if (!_userRepository.UserExist(UserName))
                return NotFound();

            var userToDelete = _userRepository.GetUser(UserName);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar al usuario {userToDelete.UserName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
