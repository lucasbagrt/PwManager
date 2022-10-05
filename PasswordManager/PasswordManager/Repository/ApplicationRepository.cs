using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PasswordManager.Helpers;
using PasswordManager.Model;
using PasswordManager.Model.Context;
using PasswordManager.Model.RequestResponse.Application;
using PasswordManager.Repository.IRepository;
using PasswordManager.ValueObjects;

namespace PasswordManager.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationRepository(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApplicationVO> Create(SaveAppRequest model)
        {
            try
            {             
                var vo = new ApplicationVO()
                {
                   icon = model.icon,
                   name = model.name
                };
                Application app = _mapper.Map<Application>(vo);
                _context.Applications.Add(app);
                await _context.SaveChangesAsync();

                return _mapper.Map<ApplicationVO>(app);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {                
                Application app = await _context.Applications.Where(t => t.id == id).FirstOrDefaultAsync();
                if (app == null) return false;
                _context.Applications.Remove(app);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ApplicationVO>> FindAll()
        {
            try
            {                
                List<Application> applications = await _context.Applications.ToListAsync();
                return _mapper.Map<List<ApplicationVO>>(applications);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<string>> FindAllAutoComplete()
        {
            try
            {
                List<Application> applications = await _context.Applications.ToListAsync();
                var apps = _mapper.Map<List<ApplicationVO>>(applications);
                return apps.Select(t => t.name).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ApplicationVO> FindByName(string name)
        {
            try
            {
                Application app = await _context.Applications.Where(t => t.name == name).FirstOrDefaultAsync();
                return _mapper.Map<ApplicationVO>(app);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ApplicationVO> FindById(int id)
        {
            try
            {
                Application app = await _context.Applications.Where(t => t.id == id).FirstOrDefaultAsync();
                return _mapper.Map<ApplicationVO>(app);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationVO> Update(ApplicationVO vo)
        {
            try
            {
                Application app = _mapper.Map<Application>(vo);
                _context.Applications.Update(app);
                await _context.SaveChangesAsync();

                return _mapper.Map<ApplicationVO>(app);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
