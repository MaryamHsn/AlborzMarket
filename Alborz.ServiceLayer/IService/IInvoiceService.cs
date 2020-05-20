using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IInvoiceService
    {
        void AddNewInvoice(InvoiceTbl Invoice);
        IList<InvoiceTbl> GetAllInvoices();
        InvoiceTbl GetInvoice(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewInvoiceAsync(InvoiceTbl Invoice, CancellationToken ct = new CancellationToken());
        Task<IList<InvoiceTbl>> GetAllInvoicesAsync(CancellationToken ct = new CancellationToken());
        Task<InvoiceTbl> GetInvoiceAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}