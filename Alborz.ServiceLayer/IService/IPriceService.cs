using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IPriceService
    {
        void AddNewPrice(PriceTbl Price);
        IList<PriceTbl> GetAllPrices();
        PriceTbl GetPrice(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewPriceAsync(PriceTbl Price, CancellationToken ct = new CancellationToken());
        Task<IList<PriceTbl>> GetAllPricesAsync(CancellationToken ct = new CancellationToken());
        Task<PriceTbl> GetPriceAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}