using RestFullPostgre.Dto;
using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Repositories;

namespace RestFullPostgre.Services.Impl
{
    public class TrancodeInformationService : ITrancodeInformationService
    {
        private readonly IGenericRepository<TrancodeInformation> _repository;

        public TrancodeInformationService(IGenericRepository<TrancodeInformation> repository)
        {
            _repository = repository;
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

        public async Task<ResponseEntity> GetAllTrancodeName()
        {
            try
            {
                string query = "select * from trancode_catalog.tc_master_information";

                var result = await _repository.FindAllByAsync(query, new { });
                if (result.Count == 0)
                {
                    throw new Exception("Trancode name not found");
                }

                var listNameTrancode = result.Select(x 
                    => new TrancodeNameDto { name_trancode = x.name_trancode }).ToList();

                return new ResponseEntity { isSuccess = true, data = listNameTrancode, message = "Trancode name found" };
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
    }
}
