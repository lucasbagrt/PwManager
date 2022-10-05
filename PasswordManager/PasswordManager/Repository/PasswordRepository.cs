using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PasswordManager.Helpers;
using PasswordManager.Model;
using PasswordManager.Model.Context;
using PasswordManager.Model.RequestResponse.Password;
using PasswordManager.Repository.IRepository;
using PasswordManager.ValueObjects;

namespace PasswordManager.Repository
{
    public class PasswordRepository : IPasswordRepository
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PasswordRepository(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PasswordVO> Create(SaveRequest model)
        {
            try
            {
                var userVO = (UserVO)_httpContextAccessor.HttpContext.Items["User"];
                var vo = new PasswordVO()
                {
                    app_id = model.app_id,
                    username = model.username,
                    password = EncryptionHelper.Encrypt(model.password, _appSettings.Key),
                    user_id = userVO.id,
                    Application = _context.Applications.FirstOrDefault(t => t.id == model.app_id),
                    User = _context.Users.FirstOrDefault(x => x.id == userVO.id)
                };
                Password pw = _mapper.Map<Password>(vo);
                _context.Passwords.Add(pw);
                await _context.SaveChangesAsync(); ;

                return _mapper.Map<PasswordVO>(pw);
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
                var userVO = (UserVO)_httpContextAccessor.HttpContext.Items["User"];
                Password pw = await _context.Passwords.Where(t => t.id == id).FirstOrDefaultAsync();
                if (pw == null || pw.User.id != userVO.id) return false;
                _context.Passwords.Remove(pw);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PasswordVO>> FindAll(int app_id, string username)
        {
            try
            {
                var userVO = (UserVO)_httpContextAccessor.HttpContext.Items["User"];
                List<Password> passwords = await _context.Passwords.Include(x => x.User).Include(x => x.Application).ToListAsync();
                passwords = passwords.Where
                    (t => t.User.id == userVO.id && ((app_id == 0 || t.Application.id == app_id)
                    || (string.IsNullOrEmpty(username) || t.username.Contains(username)))).ToList();
                return _mapper.Map<List<PasswordVO>>(passwords);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<dynamic>> FindAllGrouped()
        {
            try
            {
                var userVO = (UserVO)_httpContextAccessor.HttpContext.Items["User"];
                List<Password> passwords = await _context.Passwords.Include(x => x.User).Include(x => x.Application).ToListAsync();
                passwords = passwords.Where
                    (t => t.User.id == userVO.id).ToList();
                return (from a in passwords
                        group a by new { a.Application.id, a.Application.name, a.Application.icon } into g
                        select new
                        {
                            mode = "span",
                            label = string.Format("<i class='{0}'></i>", g.Key.icon) + " " + g.Key.name,
                            html = true,
                            count = g.ToList().Count,
                            children = (from b in g.ToList()
                                        select new
                                        {
                                            username = b.username,
                                            password = b.password,
                                            realPassword = DecryptPw(b.password),
                                            showPassword = false,
                                        })
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PasswordVO> FindById(int id)
        {
            try
            {
                Password pw = await _context.Passwords.Where(t => t.id == id).FirstOrDefaultAsync();
                return _mapper.Map<PasswordVO>(pw);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<PasswordVO> Update(PasswordVO vo)
        {
            try
            {
                var userVO = (UserVO)_httpContextAccessor.HttpContext.Items["User"];
                vo.user_id = userVO.id;
                Password pw = _mapper.Map<Password>(vo);
                _context.Passwords.Update(pw);
                await _context.SaveChangesAsync();

                return _mapper.Map<PasswordVO>(pw);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DecryptPw(string encryptedPw)
        {
            try
            {
                return EncryptionHelper.Decrypt(encryptedPw, _appSettings.Key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
