using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{
    class ProductDetailService
    {
    }
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
        public async Task<List<ProductDetailDTO>> GetAllCategoriesAsync(CancellationToken ct = new CancellationToken())
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
        //public async Task<List<ProductDetailDTO>> GetCategoriesBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken())
        //{
        //    var productDetail = await GetAllCategoriesAsync();
        //    return productDetail.Where(s => s..Contains(searchItem)).ToList();
        //}
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
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var ProductDetail = await _uow.ProductDetailRepository.GetAsync(id, ct);
            var obj = await _uow.ProductDetailRepository.SoftDeleteAsync(ProductDetail);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
