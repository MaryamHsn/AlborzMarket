using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IPropertyService
    {
        void AddNewProperty(PropertyTbl Property);
        IList<PropertyTbl> GetAllProperties();
        PropertyTbl GetProperty(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewPropertyAsync(PropertyTbl Property, CancellationToken ct = new CancellationToken());
        Task<IList<PropertyTbl>> GetAllPropertiesAsync(CancellationToken ct = new CancellationToken());
        Task<PropertyTbl> GetPropertyAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}