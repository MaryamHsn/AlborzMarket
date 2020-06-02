using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IOrderProcessService
    {
        void AddNewOrderProcess(OrderProcessTbl OrderProcess);
        IList<OrderProcessTbl> GetAllOrderProcesss();
        OrderProcessTbl GetOrderProcess(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewOrderProcessAsync(OrderProcessTbl OrderProcess, CancellationToken ct = new CancellationToken());
        Task<IList<OrderProcessTbl>> GetAllOrderProcesssAsync(CancellationToken ct = new CancellationToken());
        Task<OrderProcessTbl> GetOrderProcessAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}