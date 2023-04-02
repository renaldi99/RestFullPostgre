using RestFullPostgre.Dto;
using RestFullPostgre.Message;
using RestFullPostgre.Models;

namespace RestFullPostgre.Services
{
    public interface ITrancodeInformationService
    {
        Task<int> InsertListTrancode(List<TrancodeInformation> entity);
        Task<bool> CheckTrancodeNameExist(string nameTrancode);
        Task<ResponseEntity> GetAllTrancodeName();
    }
}
