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
    public class PaymentService : IPaymentService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public PaymentService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewPayment(PaymentTbl Payment)
        {
            _uow.PaymentRepository.Add(Payment);
            _uow.SaveAllChanges();
        }
        public IList<PaymentTbl> GetAllPayments()
        {
            return _uow.PaymentRepository.GetAll().ToList();
        }
        public PaymentTbl GetPayment(int? id)
        {
            return _uow.PaymentRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            PaymentTbl Payment = _uow.PaymentRepository.Get(id);
            var t = _uow.PaymentRepository.SoftDelete(Payment);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewPaymentAsync(PaymentTbl Payment, CancellationToken ct = new CancellationToken())
        {
            await _uow.PaymentRepository.AddAsync(Payment, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<PaymentTbl>> GetAllPaymentsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PaymentRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<PaymentTbl> GetPaymentAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PaymentRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Payment = await _uow.PaymentRepository.GetAsync(id, ct);
            var obj = await _uow.PaymentRepository.SoftDeleteAsync(Payment);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
