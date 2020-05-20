using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IOrderOperationService
    {
        void AddNewOrderOperation(OrderOperationTbl OrderOperation);
        IList<OrderOperationTbl> GetAllOrderOperations();
        OrderOperationTbl GetOrderOperation(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewOrderOperationAsync(OrderOperationTbl OrderOperation, CancellationToken ct = new CancellationToken());
        Task<IList<OrderOperationTbl>> GetAllOrderOperationsAsync(CancellationToken ct = new CancellationToken());
        Task<OrderOperationTbl> GetOrderOperationAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}