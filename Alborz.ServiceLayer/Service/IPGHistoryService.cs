using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;

namespace Alborz.ServiceLayer.Service
{ 
    public class IPGHistoryService : IIPGHistoryService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public IPGHistoryService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewIPGHistory(IPGHistoryTbl IPGHistory)
        {
            _uow.IPGHistoryRepository.Add(IPGHistory);
            _uow.SaveAllChanges();
        }
        public IList<IPGHistoryTbl> GetAllIPGHistories()
        {
            return _uow.IPGHistoryRepository.GetAll().ToList();
        }
        public IPGHistoryTbl GetIPGHistory(int? id)
        {
            return _uow.IPGHistoryRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            IPGHistoryTbl IPGHistory = _uow.IPGHistoryRepository.Get(id);
            var t = _uow.IPGHistoryRepository.SoftDelete(IPGHistory);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewIPGHistoryAsync(IPGHistoryTbl IPGHistory, CancellationToken ct = new CancellationToken())
        {
            await _uow.IPGHistoryRepository.AddAsync(IPGHistory, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<IPGHistoryTbl>> GetAllIPGHistoriesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.IPGHistoryRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<IPGHistoryTbl> GetIPGHistoryAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.IPGHistoryRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var IPGHistory = await _uow.IPGHistoryRepository.GetAsync(id, ct);
            var obj = await _uow.IPGHistoryRepository.SoftDeleteAsync(IPGHistory);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
