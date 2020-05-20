using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{ 
    public class InvoiceService : IInvoiceService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public InvoiceService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewInvoice(InvoiceTbl Invoice)
        {
            _uow.InvoiceRepository.Add(Invoice);
            _uow.SaveAllChanges();
        }
        public IList<InvoiceTbl> GetAllInvoices()
        {
            return _uow.InvoiceRepository.GetAll().ToList();
        }
        public InvoiceTbl GetInvoice(int? id)
        {
            return _uow.InvoiceRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            InvoiceTbl Invoice = _uow.InvoiceRepository.Get(id);
            var t = _uow.InvoiceRepository.SoftDelete(Invoice);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewInvoiceAsync(InvoiceTbl Invoice, CancellationToken ct = new CancellationToken())
        {
            await _uow.InvoiceRepository.AddAsync(Invoice, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<InvoiceTbl>> GetAllInvoicesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.InvoiceRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<InvoiceTbl> GetInvoiceAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.InvoiceRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Invoice = await _uow.InvoiceRepository.GetAsync(id, ct);
            var obj = await _uow.InvoiceRepository.SoftDeleteAsync(Invoice);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
