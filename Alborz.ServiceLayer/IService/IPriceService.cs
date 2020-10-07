using Alborz.DomainLayer.DTO;
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
        Task AddNewPriceAsync(PriceDTO Price, CancellationToken ct = new CancellationToken());
        Task<List<PriceDTO>> GetAllPricesAsync(CancellationToken ct = new CancellationToken());
        Task<PriceDTO> GetPriceAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
        Task<List<PriceDTO>> GetAllPricesOfProductDetailAsync(int productDetailId, CancellationToken ct = new CancellationToken());
        Task<PriceDTO> GetLastPriceProductDetailAsync(int? productDetailId, CancellationToken ct = new CancellationToken());
        Task<IList<PriceDTO>> GetAllPricesOfProductAsync(int productId, CancellationToken ct = new CancellationToken());
        Task<PriceDTO> GetLastPriceProductAsync(int? productId, CancellationToken ct = new CancellationToken());
        PriceDTO GetLastPriceProduct(int productId);
    }
}