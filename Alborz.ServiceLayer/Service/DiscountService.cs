using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;t
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{
    public class DiscountService : IDiscountService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public DiscountService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewDiscount(DiscountTbl Discount)
        {
            _uow.DiscountRepository.Add(Discount);
            _uow.SaveAllChanges();
        }
        public IList<DiscountTbl> GetAllDiscounts()
        {
            return _uow.DiscountRepository.GetAll().ToList();
        }
        public DiscountTbl GetDiscount(int? id)
        {
            return _uow.DiscountRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            DiscountTbl Discount = _uow.DiscountRepository.Get(id);
            var t = _uow.DiscountRepository.SoftDelete(Discount);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewDiscountAsync(DiscountTbl Discount, CancellationToken ct = new CancellationToken())
        {
            await _uow.DiscountRepository.AddAsync(Discount, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<DiscountTbl>> GetAllDiscountsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.DiscountRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<DiscountTbl> GetDiscountAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.DiscountRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Discount = await _uow.DiscountRepository.GetAsync(id, ct);
            var obj = await _uow.DiscountRepository.SoftDeleteAsync(Discount);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
