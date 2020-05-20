using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{
   
    public class CartItemService : ICartItemService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public CartItemService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewCartItem(CartItemTbl CartItem)
        {
            _uow.CartItemRepository.Add(CartItem);
            _uow.SaveAllChanges();
        }
        public IList<CartItemTbl> GetAllCartItems()
        {
            return _uow.CartItemRepository.GetAll().ToList();
        }
        public CartItemTbl GetCartItem(int? id)
        {
            return _uow.CartItemRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            CartItemTbl CartItem = _uow.CartItemRepository.Get(id);
            var t = _uow.CartItemRepository.SoftDelete(CartItem);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewCartItemAsync(CartItemTbl CartItem, CancellationToken ct = new CancellationToken())
        {
            await _uow.CartItemRepository.AddAsync(CartItem, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<CartItemTbl>> GetAllCartItemsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CartItemRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<CartItemTbl> GetCartItemAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CartItemRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var CartItem = await _uow.CartItemRepository.GetAsync(id, ct);
            var obj = await _uow.CartItemRepository.SoftDeleteAsync(CartItem);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
