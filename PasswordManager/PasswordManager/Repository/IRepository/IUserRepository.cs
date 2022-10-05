using PasswordManager.Model.RequestResponse.User;
using PasswordManager.ValueObjects;

namespace PasswordManager.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserVO>> FindAll();
        Task<UserVO> FindById(int id);
        Task<UserVO> GetUserByToken();
        Task<UserVO> Create(CreateRequest model);
        Task<UserVO> Update(UserVO vo);
        Task<bool> Delete(int id);        
        Task<UserVO> Authenticate(AuthenticateRequest model);
    }
}
