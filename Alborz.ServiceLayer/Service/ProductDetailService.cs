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
    public class ProductDetailService : IProductDetailService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public ProductDetailService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public async Task<ProductDetailDTO> AddNewProductDetailAsync(ProductDetailDTO ProductDetail, CancellationToken ct = new CancellationToken())
        {
            try
            {
                if (ProductDetail == null)
                    throw new ArgumentNullException();
                var entity = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(ProductDetail);
                var obj = await _uow.ProductDetailRepository.AddAsync(entity, ct);
                _uow.SaveAllChanges();
                var element = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(obj);
                return element;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
        public async Task AddAllProductDetailsAsync(List<ProductDetailDTO> productDetails, CancellationToken ct = new CancellationToken())
        {
            try
            {
                if (productDetails == null)
                    throw new ArgumentNullException();
                foreach (var item in productDetails)
                {
                    var entity = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(item);
                    var obj = await _uow.ProductDetailRepository.AddAsync(entity, ct);
                }
                _uow.SaveAllChanges(); 
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
        public async Task<List<ProductDetailDTO>> GetAllProductDetailsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ProductDetailRepository.GetAllAsync(ct);
            var entity = new List<ProductDetailDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(item);
                entity.Add(element);
            }
            return entity;
        }
         public async Task<List<ProductDetailDTO>> GetAllProductDetailByProductIdAsync(int productId,CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ProductDetailRepository.GetAllAsync(x => x.ProductId== productId,ct);
            var entity = new List<ProductDetailDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(item);
                entity.Add(element);
            }
            return entity;
        }
         public async Task<ProductDetailDTO> GetProductDetailAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ProductDetailRepository.GetAllAsync(x => x.Id == id);
            var element = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(obj.FirstOrDefault());
            return element;
        }
        public async Task<ProductDetailDTO> UpdateProductDetailAsync(ProductDetailDTO entity)
        {
            var obj = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(entity);
            obj.IsActive = true;
            obj = await _uow.ProductDetailRepository.UpdateAsync(obj);
            _uow.SaveAllChanges();
            var element = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(obj);
            return element;
        }
        public async Task UpdateAllProductDetailAsync(List<ProductDetailDTO> entity)
        {
            foreach (var item in entity)
            {
                var obj = BaseMapper<ProductDetailDTO, ProductDetailTbl>.Map(item);
                obj.IsActive = true;
                obj = await _uow.ProductDetailRepository.UpdateAsync(obj);
            }
            
            _uow.SaveAllChanges(); 
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var ProductDetail = await _uow.ProductDetailRepository.GetAsync(id, ct);
            var obj = await _uow.ProductDetailRepository.SoftDeleteAsync(ProductDetail);
            _uow.SaveAllChanges();
            return obj;
        }
        public async Task DeleteByProductIdAsync(int productId, CancellationToken ct = new CancellationToken())
        {
            var productDetail = await _uow.ProductDetailRepository.GetAllAsync(x=>x.ProductId==productId, ct);
            foreach (var item in productDetail)
            {
                var obj = await _uow.ProductDetailRepository.SoftDeleteAsync(item);
            }
            _uow.SaveAllChanges();
        }
    }
}
