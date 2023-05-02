using RestFullPostgre.Message;
using RestFullPostgre.Models;

namespace RestFullPostgre.Services
{
    public interface ISquadRelatedService
    {
        Task<int> CreateSquadRelated(SquadRelated entity);
        Task<ResponseEntity> GetAllSquadRelated();
        Task<bool> CheckSquadRelatedExist(string squadRelated);
    }
}
