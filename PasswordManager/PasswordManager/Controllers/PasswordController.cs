using Microsoft.AspNetCore.Mvc;
using PasswordManager.Authorization;
using PasswordManager.Model;
using PasswordManager.Model.RequestResponse.Password;
using PasswordManager.Repository.IRepository;
using PasswordManager.ValueObjects;

namespace PasswordManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private IPasswordRepository _repository;
        public PasswordController(IPasswordRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PasswordVO>>> FindAll(int app_id, string username)
        {
            var passwords = await _repository.FindAll(app_id, username);
            return Ok(passwords);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PasswordVO>>> FindAllGrouped()
        {
            var passwords = await _repository.FindAllGrouped();
            return Ok(passwords);
        }


        [HttpGet("{id}")]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<PasswordVO>> FindById(int id)
        {
            var pw = await _repository.FindById(id);
            if (pw == null) return NotFound();
            return Ok(pw);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PasswordVO>> Create([FromBody] SaveRequest vo)
        {
            if (vo == null || vo.app_id == 0) return BadRequest();
            var pw = await _repository.Create(vo);
            return Ok(pw);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<PasswordVO>> Update([FromBody] PasswordVO vo)
        {
            if (vo == null) return BadRequest();
            var pw = await _repository.Update(vo);
            return Ok(pw);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<PasswordVO>> Delete(int id)
        {
            var status = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
        [HttpPost("/decrypt")]
        [Authorize]
        public ActionResult<string> DecryptPw([FromBody] DecryptRequest vo)
        {
            var pw = _repository.DecryptPw(vo.password);
            if (string.IsNullOrEmpty(pw)) return NotFound();
            return Ok(pw);
        }
    }
}
