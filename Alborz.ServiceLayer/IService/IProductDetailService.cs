using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{
    public interface  IProductDetailService
    {
        Task<ProductDetailDTO> AddNewProductDetailAsync(ProductDetailDTO ProductDetail, CancellationToken ct = new CancellationToken());
        Task AddAllProductDetailsAsync(List<ProductDetailDTO> productDetails, CancellationToken ct = new CancellationToken()); Task<List<ProductDetailDTO>> GetAllProductDetailsAsync(CancellationToken ct = new CancellationToken());
        Task<List<ProductDetailDTO>> GetAllProductDetailByProductIdAsync(int productId, CancellationToken ct = new CancellationToken());
        Task<ProductDetailDTO> GetProductDetailAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<ProductDetailDTO> UpdateProductDetailAsync(ProductDetailDTO entity);
        Task UpdateAllProductDetailAsync(List<ProductDetailDTO> entity);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
        Task DeleteByProductIdAsync(int productId, CancellationToken ct = new CancellationToken());
    }
}
