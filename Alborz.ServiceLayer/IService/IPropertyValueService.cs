using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IPropertyValueService
    {
        void AddNewPropertyValue(PropertyValueTbl PropertyValue);
        IList<PropertyValueTbl> GetAllPropertyValues();
        PropertyValueTbl GetPropertyValue(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewPropertyValueAsync(PropertyValueTbl PropertyValue, CancellationToken ct = new CancellationToken());
        Task<IList<PropertyValueTbl>> GetAllPropertyValuesAsync(CancellationToken ct = new CancellationToken());
        Task<PropertyValueTbl> GetPropertyValueAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}