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
    class InvoiceProcessService
    {
    }
    public class InvoiceProcessService : IInvoiceProcessService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public InvoiceProcessService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewInvoiceProcess(InvoiceProcessTbl InvoiceProcess)
        {
            _uow.InvoiceProcessRepository.Add(InvoiceProcess);
            _uow.SaveAllChanges();
        }
        public IList<InvoiceProcessTbl> GetAllInvoiceProcesss()
        {
            return _uow.InvoiceProcessRepository.GetAll().ToList();
        }
        public InvoiceProcessTbl GetInvoiceProcess(int? id)
        {
            return _uow.InvoiceProcessRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            InvoiceProcessTbl InvoiceProcess = _uow.InvoiceProcessRepository.Get(id);
            var t = _uow.InvoiceProcessRepository.SoftDelete(InvoiceProcess);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewInvoiceProcessAsync(InvoiceProcessTbl InvoiceProcess, CancellationToken ct = new CancellationToken())
        {
            await _uow.InvoiceProcessRepository.AddAsync(InvoiceProcess, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<InvoiceProcessTbl>> GetAllInvoiceProcesssAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.InvoiceProcessRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<InvoiceProcessTbl> GetInvoiceProcessAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.InvoiceProcessRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var InvoiceProcess = await _uow.InvoiceProcessRepository.GetAsync(id, ct);
            var obj = await _uow.InvoiceProcessRepository.SoftDeleteAsync(InvoiceProcess);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
