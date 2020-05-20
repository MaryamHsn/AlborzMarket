using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;

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
        public void AddNewProduct(ProductTbl Product)
        {
            _uow.ProductRepository.Add(Product);
            _uow.SaveAllChanges();
        }
        public IList<ProductTbl> GetAllProducts()
        {
            return _uow.ProductRepository.GetAll().ToList();
        }
        public ProductTbl GetProduct(int? id)
        {
            return _uow.ProductRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            ProductTbl Product = _uow.ProductRepository.Get(id);
            var t = _uow.ProductRepository.SoftDelete(Product);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewProductAsync(ProductTbl Product, CancellationToken ct = new CancellationToken())
        {
            await _uow.ProductRepository.AddAsync(Product, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<ProductTbl>> GetAllProductsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ProductRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<ProductTbl> GetProductAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ProductRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
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
