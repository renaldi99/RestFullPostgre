using RestFullPostgre.Dto;
using RestFullPostgre.Message;
using RestFullPostgre.Models;

namespace RestFullPostgre.Services
{
    public interface ITrancodeManualService
    {
        Task<int> InsertTrancodeManual(TrancodeManual entity);
        Task<ResponseEntity> SearchTrancodeManualBy(SearchTrancodeManualDto searchTrancode);
    }
}
