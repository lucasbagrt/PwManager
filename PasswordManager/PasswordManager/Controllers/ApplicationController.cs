using Microsoft.AspNetCore.Mvc;
using PasswordManager.Authorization;
using PasswordManager.Model;
using PasswordManager.Model.RequestResponse.Application;
using PasswordManager.Repository.IRepository;
using PasswordManager.ValueObjects;

namespace PasswordManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private IApplicationRepository _repository;
        public ApplicationController(IApplicationRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ApplicationVO>>> FindAll()
        {
            var Passwords = await _repository.FindAll();
            return Ok(Passwords);
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> FindAllAutoComplete()
        {
            var Passwords = await _repository.FindAllAutoComplete();
            return Ok(Passwords);
        }
        [HttpGet("{id}")]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<ApplicationVO>> FindById(int id)
        {
            var app = await _repository.FindById(id);
            if (app == null) return NotFound();
            return Ok(app);
        }
        [HttpGet("[action]")]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<ApplicationVO>> FindByName(string name)
        {
            var app = await _repository.FindByName(name);
            if (app == null) return NotFound();
            return Ok(app);
        }
        [HttpPost]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<ApplicationVO>> Create([FromBody] SaveAppRequest vo)
        {
            if (vo == null) return BadRequest();
            var app = await _repository.Create(vo);
            return Ok(app);
        }

        [HttpPut]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<ApplicationVO>> Update([FromBody] ApplicationVO vo)
        {
            if (vo == null) return BadRequest();
            var app = await _repository.Update(vo);
            return Ok(app);
        }

        [HttpDelete("{id}")]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<ApplicationVO>> Delete(int id)
        {
            var status = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }      
    }
}
