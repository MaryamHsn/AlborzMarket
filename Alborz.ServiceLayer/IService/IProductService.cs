using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IProductService
    {
        void AddNewProduct(ProductTbl Product);
        IList<ProductTbl> GetAllProducts();
        ProductTbl GetProduct(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewProductAsync(ProductTbl Product, CancellationToken ct = new CancellationToken());
        Task<IList<ProductTbl>> GetAllProductsAsync(CancellationToken ct = new CancellationToken());
        Task<ProductTbl> GetProductAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}