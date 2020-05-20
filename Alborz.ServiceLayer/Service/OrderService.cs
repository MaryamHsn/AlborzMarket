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
    public class OrderService : IOrderService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public OrderService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewOrder(OrderTbl Order)
        {
            _uow.OrderRepository.Add(Order);
            _uow.SaveAllChanges();
        }
        public IList<OrderTbl> GetAllOrders()
        {
            return _uow.OrderRepository.GetAll().ToList();
        }
        public OrderTbl GetOrder(int? id)
        {
            return _uow.OrderRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            OrderTbl Order = _uow.OrderRepository.Get(id);
            var t = _uow.OrderRepository.SoftDelete(Order);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewOrderAsync(OrderTbl Order, CancellationToken ct = new CancellationToken())
        {
            await _uow.OrderRepository.AddAsync(Order, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<OrderTbl>> GetAllOrdersAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.OrderRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<OrderTbl> GetOrderAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.OrderRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Order = await _uow.OrderRepository.GetAsync(id, ct);
            var obj = await _uow.OrderRepository.SoftDeleteAsync(Order);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
