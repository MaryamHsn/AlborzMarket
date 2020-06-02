using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IPaymentService
    {
        void AddNewPayment(PaymentTbl Payment);
        IList<PaymentTbl> GetAllPayments();
        PaymentTbl GetPayment(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewPaymentAsync(PaymentTbl Payment, CancellationToken ct = new CancellationToken());
        Task<IList<PaymentTbl>> GetAllPaymentsAsync(CancellationToken ct = new CancellationToken());
        Task<PaymentTbl> GetPaymentAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}