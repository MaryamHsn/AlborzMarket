using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{
    public class CartService : ICartService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public CartService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewCart(CartTbl Cart)
        {
            _uow.CartRepository.Add(Cart);
            _uow.SaveAllChanges();
        }
        public IList<CartTbl> GetAllCarts()
        {
            return _uow.CartRepository.GetAll().ToList();
        }
        public CartTbl GetCart(int? id)
        {
            return _uow.CartRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            CartTbl Cart = _uow.CartRepository.Get(id);
            var t = _uow.CartRepository.SoftDelete(Cart);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewCartAsync(CartTbl Cart, CancellationToken ct = new CancellationToken())
        {
            await _uow.CartRepository.AddAsync(Cart, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<CartTbl>> GetAllCartsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CartRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<CartTbl> GetCartAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CartRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Cart = await _uow.CartRepository.GetAsync(id, ct);
            var obj = await _uow.CartRepository.SoftDeleteAsync(Cart);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
