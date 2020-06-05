using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{
    public interface IPropertyService
    {
        Task<PropertyDTO> AddNewPropertyAsync(PropertyDTO Property, CancellationToken ct = new CancellationToken());
        Task AddAllPropertiesAsync(List<PropertyDTO> Properties, CancellationToken ct = new CancellationToken());
        Task<List<PropertyDTO>> GetAllPropertysAsync(CancellationToken ct = new CancellationToken());
        Task<List<PropertyDTO>> GetAllPropertiesByProductIdAsync(int? productId, CancellationToken ct = new CancellationToken());
        Task<List<PropertyDTO>> GetAllPropertiesByCategoryIdAsync(int categoryId, CancellationToken ct = new CancellationToken());
        Task<List<PropertyDTO>> GetPropertysBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken());
        Task<PropertyDTO> GetPropertyAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<PropertyDTO> UpdatePropertyAsync(PropertyDTO entity);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}