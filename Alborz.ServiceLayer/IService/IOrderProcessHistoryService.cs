using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IOrderProcessHistoryService
    {
        void AddNewOrderProcessHistory(OrderProcessHistoryTbl OrderProcessHistory);
        IList<OrderProcessHistoryTbl> GetAllOrderProcessHistories();
        OrderProcessHistoryTbl GetOrderProcessHistory(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewOrderProcessHistoryAsync(OrderProcessHistoryTbl OrderProcessHistory, CancellationToken ct = new CancellationToken());
        Task<IList<OrderProcessHistoryTbl>> GetAllOrderProcessHistoriesAsync(CancellationToken ct = new CancellationToken());
        Task<OrderProcessHistoryTbl> GetOrderProcessHistoryAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}