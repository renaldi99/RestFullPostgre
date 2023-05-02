using RestFullPostgre.Message;
using RestFullPostgre.Models;

namespace RestFullPostgre.Services
{
    public interface ICallerLanguageService
    {
        Task<int> CreateCallerLanguage(CallerLanguage entity);
        Task<ResponseEntity> GetAllCallerLanguage();
        Task<bool> CheckCallerLanguageExist(string callerLanguage);
    }
}
