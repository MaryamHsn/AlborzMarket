using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IInvoiceProcessHistoryService
    {
        void AddNewInvoiceProcessHistory(InvoiceProcessHistoryTbl InvoiceProcessHistory);
        IList<InvoiceProcessHistoryTbl> GetAllInvoiceProcessHistories();
        InvoiceProcessHistoryTbl GetInvoiceProcessHistory(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewInvoiceProcessHistoryAsync(InvoiceProcessHistoryTbl InvoiceProcessHistory, CancellationToken ct = new CancellationToken());
        Task<IList<InvoiceProcessHistoryTbl>> GetAllInvoiceProcessHistoriesAsync(CancellationToken ct = new CancellationToken());
        Task<InvoiceProcessHistoryTbl> GetInvoiceProcessHistoryAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}