using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface ICartItemService
    {
        void AddNewCartItem(CartItemTbl CartItem);
        IList<CartItemTbl> GetAllCartItems();
        CartItemTbl GetCartItem(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewCartItemAsync(CartItemTbl CartItem, CancellationToken ct = new CancellationToken());
        Task<IList<CartItemTbl>> GetAllCartItemsAsync(CancellationToken ct = new CancellationToken());
        Task<CartItemTbl> GetCartItemAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}