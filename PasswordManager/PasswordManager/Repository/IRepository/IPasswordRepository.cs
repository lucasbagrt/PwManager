using PasswordManager.Model.RequestResponse.Password;
using PasswordManager.ValueObjects;

namespace PasswordManager.Repository.IRepository
{
    public interface IPasswordRepository
    {
        Task<IEnumerable<PasswordVO>> FindAll(int app_id, string username);
        Task<IEnumerable<dynamic>> FindAllGrouped();
        Task<PasswordVO> FindById(int id);        
        Task<PasswordVO> Create(SaveRequest model);
        Task<PasswordVO> Update(PasswordVO vo);
        Task<bool> Delete(int id);        
        string DecryptPw(string encryptedPw);
    }
}
