using RestFullPostgre.Dto;
using RestFullPostgre.Message;
using RestFullPostgre.Models;

namespace RestFullPostgre.Services
{
    public interface ITrancodeInformationService
    {
        Task<int> InsertListTrancode(List<TrancodeInformation> entity);
        Task<bool> CheckTrancodeNameExist(string nameTrancode);
        Task<List<TrancodeNameDto>> GetAllTrancodeName();
        Task<List<TrancodeAttributesDto>> SearchTrancodeInformation(SearchTrancodeAttributesDto search);
        Task<List<TrancodeInformation>> SearchTrancodeInformation(SearchTrancodeInformationDto search);
        Task<List<TrancodeInformation>> GetTrancodeInformation();
    }
}
