using RestFullPostgre.Models;

namespace RestFullPostgre.Services
{
    public interface ITrancodeInformationService
    {
        Task<int> InsertListTrancode(List<TrancodeInformation> entity);
    }
}
