using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestFullPostgre.Services.Impl
{
    public class CallerLanguageService : ICallerLanguageService
    {
        private readonly IGenericRepository<CallerLanguage> _repository;

        public CallerLanguageService(IGenericRepository<CallerLanguage> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckCallerLanguageExist(string callerLanguage)
        {
            string query = "select * from trancode_catalog.tc_param_caller_language where caller_language = @callerLanguage";

            var result = await _repository.FindByAsync(query, new { callerLanguage = callerLanguage });

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<int> CreateCallerLanguage(CallerLanguage entity)
        {
            string query = $"insert into trancode_catalog.tc_param_caller_language(caller_language) values (@caller_language)";

            var result = await _repository.SaveAsync(query, entity);

            if (result == 0)
            {
                throw new Exception("Error when save caller language [service]");
            }

            return result;
        }

        public async Task<ResponseEntity> GetAllCallerLanguage()
        {
            string query = $"select * from trancode_catalog.tc_param_caller_language";

            var result = await _repository.FindAllByAsync(query, new { });

            if (result.Count == 0)
            {
                throw new Exception("Caller language not found");
            }

            return new ResponseEntity { isSuccess = true, data = result };
        }

    }
}
