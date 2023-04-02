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

        public async Task<int> InsertListTrancode(List<TrancodeInformation> entity)
        {
            try
            {
                string query = "insert into trancode_catalog.tc_master_information (name_trancode, type_trancode, description, environment) values (@name_trancode, @type_trancode, @description, @environment)";
                

                foreach (var trancode in entity)
                {
                    var result = await _repository.SaveAsync(query, entity);
                    if (result == 0)
                    {
                        throw new Exception("Error save trancode");
                    }
                }

                return 1;


            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
    }
}
