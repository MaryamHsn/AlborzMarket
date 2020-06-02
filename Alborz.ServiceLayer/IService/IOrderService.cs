using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IOrderService
    {
        void AddNewOrder(OrderTbl Order);
        IList<OrderTbl> GetAllOrders();
        OrderTbl GetOrder(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewOrderAsync(OrderTbl Order, CancellationToken ct = new CancellationToken());
        Task<IList<OrderTbl>> GetAllOrdersAsync(CancellationToken ct = new CancellationToken());
        Task<OrderTbl> GetOrderAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}