using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface ICartService
    {
        void AddNewCart(CartTbl Cart);
        IList<CartTbl> GetAllCarts();
        CartTbl GetCart(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewCartAsync(CartTbl Cart, CancellationToken ct = new CancellationToken());
        Task<IList<CartTbl>> GetAllCartsAsync(CancellationToken ct = new CancellationToken());
        Task<CartTbl> GetCartAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}