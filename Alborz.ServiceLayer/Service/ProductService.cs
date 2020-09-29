using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;
using Alborz.ServiceLayer.Mapper;
using Alborz.ServiceLayer.Utils;
using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Enumration;

namespace Alborz.ServiceLayer.Service
{
    public class ProductService : IProductService
    {
        IUnitOfWork _uow;
        DateTime _now;
        readonly IProductDetailService _productDetail;

        public ProductService(IUnitOfWork uow, IProductDetailService productDetail)
        {
            _now = DateTime.Now;
            _uow = uow;
            _productDetail = productDetail;
        }
        public async Task<ProductDTO> AddNewProductAsync(ProductDTO product, CancellationToken ct = new CancellationToken())
        {
            if (product == null)
                throw new ArgumentNullException();
            var entity = BaseMapper<ProductDTO, ProductTbl>.Map(product);
            if (!string.IsNullOrEmpty(product.StartDateString))
                entity.StartDate = product.StartDateString.ToGeorgianDate();
            if (!string.IsNullOrEmpty(product.EndDateString))
                entity.EndDate = product.EndDateString.ToGeorgianDate();
            var obj = await _uow.ProductRepository.AddAsync(entity, ct);
            _uow.SaveAllChanges();
            var element = BaseMapper<ProductDTO, ProductTbl>.Map(obj);
            if (element.StartDate != null)
                element.StartDateString = ((DateTime)(element.StartDate)).ToPersianDateString();
            if (element.EndDate != null)
                element.EndDateString = ((DateTime)(element.EndDate)).ToPersianDateString();
            return element;
        }
        public async Task<List<ProductDTO>> GetAllProductsAsync(CancellationToken ct = new CancellationToken())
        {
            //var obj = await _uow.ProductRepository.GetAllAsync(ct);
            //var entity = new List<ProductDTO>();
            //foreach (var item in obj)
            //{
            //    var productDetail =await _productDetail.GetAllProductDetailByProductIdAsync(item.Id);
            //    var element = BaseMapper<ProductDTO, ProductTbl>.Map(item);
            //    if (item.StartDate != null)
            //        element.StartDateString = ((DateTime)(item.StartDate)).ToPersianDateString();
            //    if (item.EndDate != null)
            //        element.EndDateString = ((DateTime)(item.EndDate)).ToPersianDateString();
            //    if (productDetail!=null)
            //    {
            //        foreach (var det in productDetail.ToList())
            //        {
            //            element.ProductDetails.Add(det);
            //        }
            //    }
            //}
            //return entity;
            var obj = await _uow.ProductRepository.GetAllAsync();
            var list = new List<ProductDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<ProductDTO, ProductTbl>.Map(item);
                if (element.StartDate != null)
                    element.StartDateString = ((DateTime)(element.StartDate)).ToPersianDateString();
                if (element.EndDate != null)
                    element.EndDateString = ((DateTime)(element.EndDate)).ToPersianDateString();
                list.Add(element);
            }
            return list;
        }
        public async Task<List<ProductDTO>> GetProductsBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken())
        {
            var product = await GetAllProductsAsync();
            return product.Where(s => s.Title.Contains(searchItem) || s.Code.Contains(searchItem) || s.Brand.ToString().Contains(searchItem)|| s.Categories.Select(x=>x.Title.Contains(searchItem)).FirstOrDefault()).ToList();
        }
        public async Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int? categoryId, CancellationToken ct = new CancellationToken())
        {
            var product = await _uow.ProductRepository.GetAllAsync(x=>x.CategoryId==categoryId);
            return product.Select(BaseMapper<ProductDTO, ProductTbl>.Map).ToList();
        }
        public async Task<ProductDTO> GetProductAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ProductRepository.GetAllAsync(x => x.Id == id);
            var productDetail = await _productDetail.GetAllProductDetailByProductIdAsync((int)id);
            var element = BaseMapper<ProductDTO, ProductTbl>.Map(obj.FirstOrDefault());
            return element;
        }
        public async Task<ProductDTO> UpdateProductAsync(ProductDTO entity)
        {
            var obj = BaseMapper<ProductDTO, ProductTbl>.Map(entity);
            var productDetail = await _productDetail.GetAllProductDetailByProductIdAsync(entity.Id);
            if (!string.IsNullOrEmpty(entity.StartDateString))
                obj.StartDate = entity.StartDateString.ToGeorgianDate();
            if (!string.IsNullOrEmpty(entity.EndDateString))
                obj.EndDate = entity.EndDateString.ToGeorgianDate();
            obj.IsActive = true;
            obj = await _uow.ProductRepository.UpdateAsync(obj);
            _uow.SaveAllChanges();
            var element = BaseMapper<ProductDTO, ProductTbl>.Map(obj);
            return element;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Product = await _uow.ProductRepository.GetAsync(id, ct);
            var obj = await _uow.ProductRepository.SoftDeleteAsync(Product);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
