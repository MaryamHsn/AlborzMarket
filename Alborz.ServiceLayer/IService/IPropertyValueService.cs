using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IPropertyValueService
    {
        Task<PropertyValueDTO> AddNewPropertyValueAsync(PropertyValueDTO PropertyValue, CancellationToken ct = new CancellationToken());
        Task<List<PropertyValueDTO>> GetAllPropertyValuesAsync(CancellationToken ct = new CancellationToken());
        Task<List<PropertyValueDTO>> GetPropertyValuesBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken());
        Task<PropertyValueDTO> GetPropertyValueAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<PropertyValueDTO> UpdatePropertyValueAsync(PropertyValueDTO entity);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}