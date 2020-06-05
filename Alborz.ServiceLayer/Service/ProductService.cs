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
        public ProductService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public async Task<ProductDTO> AddNewProductAsync(ProductDTO Product, CancellationToken ct = new CancellationToken())
        {
            if (Product == null)
                throw new ArgumentNullException();
            var entity = BaseMapper<ProductDTO, ProductTbl>.Map(Product);
            if (!string.IsNullOrEmpty(Product.StartDateString))
                entity.StartDate = Product.StartDateString.ToGeorgianDate();
            if (!string.IsNullOrEmpty(Product.EndDateString))
                entity.EndDate = Product.EndDateString.ToGeorgianDate();
            entity.Color = (int)Product.ColorEnum;
            var obj = await _uow.ProductRepository.AddAsync(entity, ct);
            _uow.SaveAllChanges();
            var element = BaseMapper<ProductDTO, ProductTbl>.Map(obj);
            if (element.StartDate != null)
                element.StartDateString = ((DateTime)(element.StartDate)).ToPersianDateString();
            if (element.EndDate != null)
                element.EndDateString = ((DateTime)(element.EndDate)).ToPersianDateString();
            element.ColorEnum = Product.ColorEnum;
            return element;
        }
        public async Task<List<ProductDTO>> GetAllProductsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ProductRepository.GetAllAsync(ct);
            var entity = new List<ProductDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<ProductDTO, ProductTbl>.Map(item);
                if (item.StartDate != null)
                    element.StartDateString = ((DateTime)(item.StartDate)).ToPersianDateString();
                if (item.EndDate != null)
                    element.EndDateString = ((DateTime)(item.EndDate)).ToPersianDateString();
                element.ColorEnum= (ColorEnum)Enum.ToObject(typeof(ColorEnum), item.Color);
                entity.Add(element);
            }
            return entity;
        }
        public async Task<List<ProductDTO>> GetProductsBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken())
        {
            var product = await GetAllProductsAsync();
            return product.Where(s => s.Title.Contains(searchItem) || s.Code.Contains(searchItem) || s.Brand.ToString().Contains(searchItem)|| s.Categories.Select(x=>x.Title.Contains(searchItem)).FirstOrDefault()).ToList();
        }
        public async Task<ProductDTO> GetProductAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ProductRepository.GetAllAsync(x => x.Id == id);
            var element = BaseMapper<ProductDTO, ProductTbl>.Map(obj.FirstOrDefault());
            if (element.StartDate != null)
                element.StartDateString = ((DateTime)(element.StartDate)).ToPersianDateString();
            if (element.EndDate != null)
                element.EndDateString = ((DateTime)(element.EndDate)).ToPersianDateString();
            element.ColorEnum = (ColorEnum)Enum.ToObject(typeof(ColorEnum), obj.FirstOrDefault().Color);
            return element;
        }
        public async Task<ProductDTO> UpdateProductAsync(ProductDTO entity)
        {
            var obj = BaseMapper<ProductDTO, ProductTbl>.Map(entity);
            if (!string.IsNullOrEmpty(entity.StartDateString))
                obj.StartDate = entity.StartDateString.ToGeorgianDate();
            if (!string.IsNullOrEmpty(entity.EndDateString))
                obj.EndDate = entity.EndDateString.ToGeorgianDate();
            obj.IsActive = true;
            obj = await _uow.ProductRepository.UpdateAsync(obj);
            _uow.SaveAllChanges();
            var element = BaseMapper<ProductDTO, ProductTbl>.Map(obj);
            if (element.StartDate != null)
                element.StartDateString = ((DateTime)(element.StartDate)).ToPersianDateString();
            if (element.EndDate != null)
                element.EndDateString = ((DateTime)(element.EndDate)).ToPersianDateString();
            element.ColorEnum = entity.ColorEnum;
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
