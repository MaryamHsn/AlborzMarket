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
    public class PriceService : IPriceService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public PriceService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewPrice(PriceTbl Price)
        {
            _uow.PriceRepository.Add(Price);
            _uow.SaveAllChanges();
        }
        public IList<PriceTbl> GetAllPrices()
        {
            return _uow.PriceRepository.GetAll().ToList();
        }
        public PriceTbl GetPrice(int? id)
        {
            return _uow.PriceRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            PriceTbl Price = _uow.PriceRepository.Get(id);
            var t = _uow.PriceRepository.SoftDelete(Price);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewPriceAsync(PriceTbl Price, CancellationToken ct = new CancellationToken())
        {
            await _uow.PriceRepository.AddAsync(Price, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<PriceTbl>> GetAllPricesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PriceRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<PriceTbl> GetPriceAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PriceRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Price = await _uow.PriceRepository.GetAsync(id, ct);
            var obj = await _uow.PriceRepository.SoftDeleteAsync(Price);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
