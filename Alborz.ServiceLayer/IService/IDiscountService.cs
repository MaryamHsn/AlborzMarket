using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IDiscountService
    {
        void AddNewDiscount(DiscountTbl Discount);
        IList<DiscountTbl> GetAllDiscounts();
        DiscountTbl GetDiscount(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewDiscountAsync(DiscountTbl Discount, CancellationToken ct = new CancellationToken());
        Task<IList<DiscountTbl>> GetAllDiscountsAsync(CancellationToken ct = new CancellationToken());
        Task<DiscountTbl> GetDiscountAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}