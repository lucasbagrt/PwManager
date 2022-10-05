using PasswordManager.Model.RequestResponse.Application;
using PasswordManager.ValueObjects;

namespace PasswordManager.Repository.IRepository
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<ApplicationVO>> FindAll();
        Task<IEnumerable<string>> FindAllAutoComplete();
        Task<ApplicationVO> FindByName(string name);
        Task<ApplicationVO> FindById(int id);        
        Task<ApplicationVO> Create(SaveAppRequest model);
        Task<ApplicationVO> Update(ApplicationVO vo);
        Task<bool> Delete(int id);        
    }
}
