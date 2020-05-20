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
    class OrderProcessHistoryService
    {
    }
    public class OrderProcessHistoryService : IOrderProcessHistoryService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public OrderProcessHistoryService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewOrderProcessHistory(OrderProcessHistoryTbl OrderProcessHistory)
        {
            _uow.OrderProcessHistoryRepository.Add(OrderProcessHistory);
            _uow.SaveAllChanges();
        }
        public IList<OrderProcessHistoryTbl> GetAllOrderProcessHistories()
        {
            return _uow.OrderProcessHistoryRepository.GetAll().ToList();
        }
        public OrderProcessHistoryTbl GetOrderProcessHistory(int? id)
        {
            return _uow.OrderProcessHistoryRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            OrderProcessHistoryTbl OrderProcessHistory = _uow.OrderProcessHistoryRepository.Get(id);
            var t = _uow.OrderProcessHistoryRepository.SoftDelete(OrderProcessHistory);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewOrderProcessHistoryAsync(OrderProcessHistoryTbl OrderProcessHistory, CancellationToken ct = new CancellationToken())
        {
            await _uow.OrderProcessHistoryRepository.AddAsync(OrderProcessHistory, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<OrderProcessHistoryTbl>> GetAllOrderProcessHistoriesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.OrderProcessHistoryRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<OrderProcessHistoryTbl> GetOrderProcessHistoryAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.OrderProcessHistoryRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var OrderProcessHistory = await _uow.OrderProcessHistoryRepository.GetAsync(id, ct);
            var obj = await _uow.OrderProcessHistoryRepository.SoftDeleteAsync(OrderProcessHistory);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
