using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;

namespace Alborz.ServiceLayer.Service
{ 
    public class OrderOperationService : IOrderOperationService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public OrderOperationService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewOrderOperation(OrderOperationTbl OrderOperation)
        {
            _uow.OrderOperationRepository.Add(OrderOperation);
            _uow.SaveAllChanges();
        }
        public IList<OrderOperationTbl> GetAllOrderOperations()
        {
            return _uow.OrderOperationRepository.GetAll().ToList();
        }
        public OrderOperationTbl GetOrderOperation(int? id)
        {
            return _uow.OrderOperationRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            OrderOperationTbl OrderOperation = _uow.OrderOperationRepository.Get(id);
            var t = _uow.OrderOperationRepository.SoftDelete(OrderOperation);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewOrderOperationAsync(OrderOperationTbl OrderOperation, CancellationToken ct = new CancellationToken())
        {
            await _uow.OrderOperationRepository.AddAsync(OrderOperation, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<OrderOperationTbl>> GetAllOrderOperationsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.OrderOperationRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<OrderOperationTbl> GetOrderOperationAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.OrderOperationRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var OrderOperation = await _uow.OrderOperationRepository.GetAsync(id, ct);
            var obj = await _uow.OrderOperationRepository.SoftDeleteAsync(OrderOperation);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
