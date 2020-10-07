﻿using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using Alborz.ServiceLayer.Mapper;
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
        public async Task AddNewPriceAsync(PriceDTO Price, CancellationToken ct = new CancellationToken())
        {
            await _uow.PriceRepository.AddAsync(BaseMapper<PriceDTO, PriceTbl>.Map(Price), ct);
            _uow.SaveAllChanges();
        }
        public async Task<List<PriceDTO>> GetAllPricesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PriceRepository.GetAllAsync(ct);
            return obj.Where(x => x.IsActive == true).Select(BaseMapper<PriceDTO,PriceTbl>.Map).ToList();
        }
        public async Task<PriceDTO> GetPriceAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PriceRepository.GetAllAsync(x => x.Id == id);
            var entity = BaseMapper<PriceDTO, PriceTbl>.Map(obj.FirstOrDefault());
            return entity;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Price = await _uow.PriceRepository.GetAsync(id, ct);
            var obj = await _uow.PriceRepository.SoftDeleteAsync(Price);
            _uow.SaveAllChanges();
            return obj;
        }
        public async Task<List<PriceDTO>> GetAllPricesOfProductDetailAsync(int productDetailId, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PriceRepository.GetAllAsync(ct);
            var elements = obj.Where(x => x.ProductDetailId== productDetailId && x.IsActive).Select(BaseMapper<PriceDTO, PriceTbl>.Map).ToList();
            return elements;
        }
        public async Task<PriceDTO> GetLastPriceProductDetailAsync(int? productDetailId, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PriceRepository.GetAllAsync(x => x.ProductDetailId == productDetailId && x.IsActive);
            var element = obj.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return BaseMapper<PriceDTO, PriceTbl>.Map(element); 
        }
          public async Task<IList<PriceDTO>> GetAllPricesOfProductAsync(int productId, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PriceRepository.GetAllAsync(ct);
            var elements = obj.Where(x => x.ProductId== productId && x.IsActive).Select(BaseMapper<PriceDTO, PriceTbl>.Map).ToList();
            return elements;
        }
        public async Task<PriceDTO> GetLastPriceProductAsync(int? productId, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PriceRepository.GetAllAsync(x => x.ProductId == productId && x.IsActive);
            var element = obj.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return BaseMapper<PriceDTO, PriceTbl>.Map(element); 
        }
        public PriceDTO GetLastPriceProduct (int productId)
        {
            var obj = _uow.PriceRepository.GetAll(x => x.ProductId == productId && x.IsActive);
            var element = obj.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return BaseMapper<PriceDTO, PriceTbl>.Map(element); 
        }

    }
}
