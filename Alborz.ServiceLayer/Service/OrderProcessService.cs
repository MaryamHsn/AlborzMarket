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
    class OrderProcessService
    {
    }
    public class OrderProcessService : IOrderProcessService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public OrderProcessService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewOrderProcess(OrderProcessTbl OrderProcess)
        {
            _uow.OrderProcessRepository.Add(OrderProcess);
            _uow.SaveAllChanges();
        }
        public IList<OrderProcessTbl> GetAllOrderProcesss()
        {
            return _uow.OrderProcessRepository.GetAll().ToList();
        }
        public OrderProcessTbl GetOrderProcess(int? id)
        {
            return _uow.OrderProcessRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            OrderProcessTbl OrderProcess = _uow.OrderProcessRepository.Get(id);
            var t = _uow.OrderProcessRepository.SoftDelete(OrderProcess);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewOrderProcessAsync(OrderProcessTbl OrderProcess, CancellationToken ct = new CancellationToken())
        {
            await _uow.OrderProcessRepository.AddAsync(OrderProcess, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<OrderProcessTbl>> GetAllOrderProcesssAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.OrderProcessRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<OrderProcessTbl> GetOrderProcessAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.OrderProcessRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var OrderProcess = await _uow.OrderProcessRepository.GetAsync(id, ct);
            var obj = await _uow.OrderProcessRepository.SoftDeleteAsync(OrderProcess);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
