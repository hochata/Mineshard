using System.Data;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Mineshard.Api.Models.DTO.User;
using Mineshard.Persistence.Models.Auth;
using Mineshard.Persistence.Repos;

namespace Mineshard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UsersController(
            IUserRepository userRepository,
            IMapper mapper,
            IRoleRepository roleRepository
        )
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User? existingUser = _userRepository.GetByUsername(userRequest.Username);

            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username already exists");
                return BadRequest(ModelState);
            }

            Role? role = _roleRepository.GetByName(userRequest.Role);

            if (role is null)
            {
                ModelState.AddModelError("Role", "Role does not exist");
                return BadRequest(ModelState);
            }

            User user = _userRepository.Create(
                new User()
                {
                    Email = userRequest.Email,
                    Name = userRequest.Name,
                    Username = userRequest.Username,
                    RoleId = role.Id,
                }
            );

            user.Role = role;
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();
            return Ok(_mapper.Map<List<UserDto>>(users));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(user);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateUserRequest userUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_userRepository.GetByUsername(userUpdate.Username) != null)
            {
                ModelState.AddModelError("Username", "Username already exists");
                return BadRequest(ModelState);
            }

            Role? role = _roleRepository.GetByName(userUpdate.Role);

            if (role is null)
            {
                ModelState.AddModelError("Role", "Role does not exist");
                return BadRequest(ModelState);
            }

            User? existingUser = _userRepository.GetById(id);

            if (existingUser is null)
                return NotFound();

            existingUser.Username = userUpdate.Username;
            existingUser.Email = userUpdate.Email;
            existingUser.Name = userUpdate.Name;
            existingUser.RoleId = role.Id;

            _userRepository.Update(existingUser);

            existingUser.Role = role;

            return Ok(_mapper.Map<UserDto>(existingUser));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            User? user = _userRepository.GetById(id);

            if (user is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}
