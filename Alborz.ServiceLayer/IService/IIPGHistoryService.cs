using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IIPGHistoryService
    {
        void AddNewIPGHistory(IPGHistoryTbl IPGHistory);
        IList<IPGHistoryTbl> GetAllIPGHistories();
        IPGHistoryTbl GetIPGHistory(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewIPGHistoryAsync(IPGHistoryTbl IPGHistory, CancellationToken ct = new CancellationToken());
        Task<IList<IPGHistoryTbl>> GetAllIPGHistoriesAsync(CancellationToken ct = new CancellationToken());
        Task<IPGHistoryTbl> GetIPGHistoryAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}