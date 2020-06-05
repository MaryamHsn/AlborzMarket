using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IProductService
    {
        Task<ProductDTO> AddNewProductAsync(ProductDTO Product, CancellationToken ct = new CancellationToken());
        Task<List<ProductDTO>> GetAllProductsAsync(CancellationToken ct = new CancellationToken());
        Task<List<ProductDTO>> GetProductsBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken());
        Task<ProductDTO> GetProductAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<ProductDTO> UpdateProductAsync(ProductDTO entity);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}