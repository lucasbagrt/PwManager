using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PasswordManager.Authorization;
using PasswordManager.Helpers;
using PasswordManager.Model;
using PasswordManager.Model.Context;
using PasswordManager.Model.RequestResponse.User;
using PasswordManager.Repository.IRepository;
using PasswordManager.ValueObjects;

namespace PasswordManager.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(DataContext context, IMapper mapper, IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserVO> Authenticate(AuthenticateRequest model)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.username == model.username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(model.password, user.password))
                    throw new AppException("Usuario ou senha invalidos");

                var jwtToken = _jwtUtils.GenerateJwtToken(user);

                return new UserVO(user, jwtToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserVO> Create(CreateRequest model)
        {
            try
            {
                var vo = new UserVO()
                {
                    name = model.name,
                    username = model.username,
                    password = BCrypt.Net.BCrypt.HashPassword(model.password),
                    Role = Role.User
                };
                User user = _mapper.Map<User>(vo);
                _context.Users.Add(user);
                await _context.SaveChangesAsync(); ;

                return _mapper.Map<UserVO>(user);
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
                User user = await _context.Users.Where(t => t.id == id).FirstOrDefaultAsync();
                if (user == null) return false;
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserVO>> FindAll()
        {
            try
            {
                List<User> users = await _context.Users.ToListAsync();
                return _mapper.Map<List<UserVO>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserVO> FindById(int id)
        {
            try
            {
                User user = await _context.Users.Where(t => t.id == id).FirstOrDefaultAsync();
                return _mapper.Map<UserVO>(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserVO> GetUserByToken()
        {
            try
            {
                var userVO = (UserVO)_httpContextAccessor.HttpContext.Items["User"];
                var user = await FindById(userVO.id);
                return _mapper.Map<UserVO>(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserVO> Update(UserVO vo)
        {
            try
            {
                User user = _mapper.Map<User>(vo);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserVO>(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}