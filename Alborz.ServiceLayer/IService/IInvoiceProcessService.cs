using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IInvoiceProcessService
    {
        void AddNewInvoiceProcess(InvoiceProcessTbl InvoiceProcess);
        IList<InvoiceProcessTbl> GetAllInvoiceProcesss();
        InvoiceProcessTbl GetInvoiceProcess(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewInvoiceProcessAsync(InvoiceProcessTbl InvoiceProcess, CancellationToken ct = new CancellationToken());
        Task<IList<InvoiceProcessTbl>> GetAllInvoiceProcesssAsync(CancellationToken ct = new CancellationToken());
        Task<InvoiceProcessTbl> GetInvoiceProcessAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}