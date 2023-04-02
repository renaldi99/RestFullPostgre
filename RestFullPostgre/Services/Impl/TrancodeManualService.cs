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

        public TrancodeManualService(IGenericRepository<TrancodeManual> repository, ITrancodeInformationService trancodeInformationService)
        {
            _repository = repository;
            _trancodeInformationService = trancodeInformationService;
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

                string query = "insert into trancode_catalog.tc_master_information_manual (name_trancode, caller_trancode, caller_language, squad_related, use) values (@name_trancode, @caller_trancode, @caller_language, @caller_language, @use)";

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
                string query = $"";
                return new ResponseEntity { isSuccess = true };
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
