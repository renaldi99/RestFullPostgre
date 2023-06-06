using RestFullPostgre.Dto;
using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Repositories;
using Sprache;

namespace RestFullPostgre.Services.Impl
{
    public class TrancodeInformationService : ITrancodeInformationService
    {
        private readonly IGenericRepository<TrancodeInformation> _repository;
        private readonly IGenericRepository<TrancodeAttributesDto> _repositoryCallAttribute;

        public TrancodeInformationService(IGenericRepository<TrancodeInformation> repository, IGenericRepository<TrancodeAttributesDto> repositoryCallAttribute)
        {
            _repository = repository;
            _repositoryCallAttribute = repositoryCallAttribute;
        }

        public async Task<bool> CheckTrancodeNameExist(string nameTrancode)
        {
            try
            {
                string query = $"select * from trancode_catalog.tc_master_information where lower(name_trancode) like lower('%{nameTrancode}%')";

                var result = await _repository.FindByAsync(query, new { });

                if (result is null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public async Task<List<TrancodeNameDto>> GetAllTrancodeName()
        {
            try
            {
                string query = "select * from trancode_catalog.tc_master_information";

                var result = await _repository.FindAllByAsync(query, new { });
                if (result.Count == 0)
                {
                    throw new Exception("Trancode name not found");
                }

                var listNameTrancode = result.Select(x => new TrancodeNameDto 
                { 
                    name_trancode = x.name_trancode 
                }).ToList();

                return listNameTrancode;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<List<TrancodeInformation>> GetTrancodeInformation()
        {
            try
            {
                string query = $"select * from trancode_catalog.tc_master_information";

                var getAllTrancodeInformation = await _repository.FindAllByAsync(query, new { });

                if (getAllTrancodeInformation.Count == 0)
                {
                    throw new Exception("Trancode not found");
                }

                return getAllTrancodeInformation;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<int> InsertListTrancode(List<TrancodeInformation> entity)
        {
            try
            {
                string query = "insert into trancode_catalog.tc_master_information (name_trancode, type_trancode, description, environment) values (@name_trancode, @type_trancode, @description, @environment)";
                

                var result = await _repository.SaveAsync(query, entity);
                if (result == 0)
                {
                    throw new Exception("Error when save trancode (service)");
                }

                return result;
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public async Task<List<TrancodeAttributesDto>> SearchTrancodeInformation(SearchTrancodeAttributesDto search)
        {
            try
            {
                string query = $"select tci.name_trancode, tci.type_trancode, tci.description, tci.environment, tcim.caller_trancode, tcim.caller_language, tcim.squad_related, tcim.use, tcim.group_trancode from trancode_catalog.tc_master_information tci left join trancode_catalog.tc_master_information_manual tcim on tci.name_trancode = tcim.name_trancode where lower(COALESCE(tci.name_trancode,'')) like lower('%{search.name_trancode}%') and lower(COALESCE(tci.type_trancode,'')) like lower('%{search.type_trancode}%') and lower(COALESCE(tci.description,'')) like lower('%{search.description}%') and lower(COALESCE(tci.environment,'')) like lower('%{search.environment}%') and lower(COALESCE(tcim.caller_trancode,'')) like lower('%{search.caller_trancode}%') and lower(COALESCE(tcim.caller_language,'')) like lower('%{search.caller_language}%') and lower(COALESCE(tcim.squad_related,'')) like lower('%{search.squad_related}%')";

                var result = await _repositoryCallAttribute.FindAllByAsync(query, new { });

                if (result.Count == 0)
                {
                    throw new Exception("Trancode not found");
                }

                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<List<TrancodeInformation>> SearchTrancodeInformation(SearchTrancodeInformationDto search)
        {
            try
            {
                string query = $"select * from trancode_catalog.tc_master_information where lower(name_trancode) like lower('%{search.name_trancode}%') and lower(type_trancode) like lower('%{search.type_trancode}%') and lower(description) like lower('%{search.description}%') and lower(environment) like lower('%{search.environment}%')";

                var result = await _repository.FindAllByAsync(query, new { });

                if (result.Count == 0)
                {
                    throw new Exception("Trancode not found");
                }

                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
