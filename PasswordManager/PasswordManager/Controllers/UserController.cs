using Microsoft.AspNetCore.Mvc;
using PasswordManager.Authorization;
using PasswordManager.Model;
using PasswordManager.Model.RequestResponse.User;
using PasswordManager.Repository.IRepository;
using PasswordManager.ValueObjects;

namespace PasswordManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<UserVO>> Authenticate(AuthenticateRequest model)
        {
            var response = await _repository.Authenticate(model);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserVO>>> FindAll()
        {
            var Users = await _repository.FindAll();
            return Ok(Users);
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<UserVO>> GetUserByToken()
        {            
            var User = await _repository.GetUserByToken();
            if (User == null) return NotFound();
            return Ok(User);
        }
        [HttpGet("{id}")]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<UserVO>> FindById(int id)
        {
            var User = await _repository.FindById(id);
            if (User == null) return NotFound();
            return Ok(User);
        }

        [HttpPost]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<UserVO>> Create([FromBody] CreateRequest vo)
        {
            if (vo == null) return BadRequest();
            var User = await _repository.Create(vo);
            return Ok(User);
        }

        [HttpPut]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<UserVO>> Update([FromBody] UserVO vo)
        {
            if (vo == null) return BadRequest();
            var User = await _repository.Update(vo);
            return Ok(User);
        }

        [HttpDelete("{id}")]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<UserVO>> Delete(int id)
        {
            var status = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}