using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Repositories;

namespace RestFullPostgre.Services.Impl
{
    public class SquadRelatedService : ISquadRelatedService
    {
        private readonly IGenericRepository<SquadRelated> _repository;

        public SquadRelatedService(IGenericRepository<SquadRelated> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckSquadRelatedExist(string squadRelated)
        {
            string query = "select * from trancode_catalog.tc_param_squad_related where squad_related = @squadRelated";

            var result = await _repository.FindByAsync(query, new { squadRelated = squadRelated });

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<int> CreateSquadRelated(SquadRelated entity)
        {
            string query = $"insert into trancode_catalog.tc_param_squad_related(squad_related) values (@squad_related)";

            var result = await _repository.SaveAsync(query, entity);

            if (result == 0)
            {
                throw new Exception("Error when save squad related [service]");
            }

            return result;
        }

        public async Task<ResponseEntity> GetAllSquadRelated()
        {
            string query = "select * from trancode_catalog.tc_param_squad_related";

            var result = await _repository.FindAllByAsync(query, new { });

            if (result.Count == 0)
            {
                throw new Exception("Squad related not found");
            }

            return new ResponseEntity { isSuccess = true, data = result };

        }
    }
}
