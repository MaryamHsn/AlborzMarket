using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;

namespace Alborz.ServiceLayer.Service
{
    public class InvoiceProcessHistoryService : IInvoiceProcessHistoryService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public InvoiceProcessHistoryService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewInvoiceProcessHistory(InvoiceProcessHistoryTbl InvoiceProcessHistory)
        {
            _uow.InvoiceProcessHistoryRepository.Add(InvoiceProcessHistory);
            _uow.SaveAllChanges();
        }
        public IList<InvoiceProcessHistoryTbl> GetAllInvoiceProcessHistories()
        {
            return _uow.InvoiceProcessHistoryRepository.GetAll().ToList();
        }
        public InvoiceProcessHistoryTbl GetInvoiceProcessHistory(int? id)
        {
            return _uow.InvoiceProcessHistoryRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            InvoiceProcessHistoryTbl InvoiceProcessHistory = _uow.InvoiceProcessHistoryRepository.Get(id);
            var t = _uow.InvoiceProcessHistoryRepository.SoftDelete(InvoiceProcessHistory);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewInvoiceProcessHistoryAsync(InvoiceProcessHistoryTbl InvoiceProcessHistory, CancellationToken ct = new CancellationToken())
        {
            await _uow.InvoiceProcessHistoryRepository.AddAsync(InvoiceProcessHistory, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<InvoiceProcessHistoryTbl>> GetAllInvoiceProcessHistoriesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.InvoiceProcessHistoryRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<InvoiceProcessHistoryTbl> GetInvoiceProcessHistoryAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.InvoiceProcessHistoryRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var InvoiceProcessHistory = await _uow.InvoiceProcessHistoryRepository.GetAsync(id, ct);
            var obj = await _uow.InvoiceProcessHistoryRepository.SoftDeleteAsync(InvoiceProcessHistory);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
