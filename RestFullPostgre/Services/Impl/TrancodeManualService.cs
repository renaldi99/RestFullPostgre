using RestFullPostgre.Dto;
using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Repositories;

namespace RestFullPostgre.Services.Impl
{
    public class TrancodeManualService : ITrancodeManualService
    {
        private readonly IGenericRepository<TrancodeManual> _repository;
        private readonly ITrancodeInformationService _trancodeInformationService;
        private readonly ICallerLanguageService _callerLanguageService;
        private readonly ISquadRelatedService _squadRelatedService;

        public TrancodeManualService(IGenericRepository<TrancodeManual> repository, ITrancodeInformationService trancodeInformationService, ICallerLanguageService callerLanguageService, ISquadRelatedService squadRelatedService)
        {
            _repository = repository;
            _trancodeInformationService = trancodeInformationService;
            _callerLanguageService = callerLanguageService;
            _squadRelatedService = squadRelatedService;
        }

        public async Task<bool> CheckTrancodeExist(string paramKey1, string paramKey2)
        {
            string query = "select * from trancode_catalog.tc_master_information_manual where name_trancode = @paramKey1 and caller_trancode = @paramKey2";

            var result = await _repository.FindByAsync(query, new { paramKey1, paramKey2 });
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<int> InsertTrancodeManual(TrancodeManual entity)
        {
            try
            {
                var checkTrancodeName = await _trancodeInformationService.CheckTrancodeNameExist(entity.name_trancode);
                if (!checkTrancodeName)
                {
                    throw new Exception("Trancode name doesn't exist in table (Trancode Information)");
                }

                var checkCallerLanguage = await _callerLanguageService.CheckCallerLanguageExist(entity.caller_language);
                if (!checkCallerLanguage)
                {
                    throw new Exception("Caller language doesn't exist in table (Caller Language)");
                }

                var checkSquadRelated = await _squadRelatedService.CheckSquadRelatedExist(entity.squad_related);
                if (!checkSquadRelated)
                {
                    throw new Exception("Squad related doesn't exist in table (Squad Related)");
                }

                var checkTrancode = await CheckTrancodeExist(entity.name_trancode, entity.caller_trancode);
                if (checkTrancode)
                {
                    string queryUpdate = $"update trancode_catalog.tc_master_information_manual set (name_trancode, caller_trancode, caller_language, squad_related, use, group_trancode) = ('{entity.name_trancode}', '{entity.caller_trancode}', '{entity.caller_language}', '{entity.squad_related}', '{entity.use}', '{entity.group_trancode}') where name_trancode = '{entity.name_trancode}' and caller_trancode = '{entity.caller_trancode}'";
                    await _repository.UpdateAsync(queryUpdate, new { });
                    return 2;
                }

                string query = "insert into trancode_catalog.tc_master_information_manual (name_trancode, caller_trancode, caller_language, squad_related, use, group_trancode) values (@name_trancode, @caller_trancode, @caller_language, @squad_related, @use, @group_trancode)";


                var result = await _repository.SaveAsync(query, entity);
                if (result == 0)
                {
                    throw new Exception("Error when save trancode manual (service)");
                }

                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<ResponseEntity> SearchTrancodeManualBy(SearchTrancodeManualDto searchTrancode)
        {
            try
            {
                string query = $"select * from trancode_catalog.tc_master_information_manual where lower(name_trancode) like lower('%{searchTrancode.name_trancode}%') and lower(caller_trancode) like lower('%{searchTrancode.caller_trancode}%') and lower(caller_language) like lower('%{searchTrancode.caller_language}%') and lower(squad_related) like lower('%{searchTrancode.squad_related}%')";

                var result = await _repository.FindAllByAsync(query, new { });
                if (result.Count == 0)
                {
                    throw new Exception("Trancode not found");
                }

                return new ResponseEntity { isSuccess = true, message = "Trancode found", data = result };
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
