using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IInternetPaymentGetwayService
    {
        void AddNewInternetPaymentGetway(InternetPaymentGetwayTbl InternetPaymentGetway);
        IList<InternetPaymentGetwayTbl> GetAllInternetPaymentGetwaies();
        InternetPaymentGetwayTbl GetInternetPaymentGetway(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewInternetPaymentGetwayAsync(InternetPaymentGetwayTbl InternetPaymentGetway, CancellationToken ct = new CancellationToken());
        Task<IList<InternetPaymentGetwayTbl>> GetAllInternetPaymentGetwaiesAsync(CancellationToken ct = new CancellationToken());
        Task<InternetPaymentGetwayTbl> GetInternetPaymentGetwayAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}