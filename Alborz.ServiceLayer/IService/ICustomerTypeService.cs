using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface ICustomerTypeService
    {
        void AddNewCustomerType(CustomerTypeTbl CustomerType);
        IList<CustomerTypeTbl> GetAllCustomerTypes();
        CustomerTypeTbl GetCustomerType(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewCustomerTypeAsync(CustomerTypeTbl CustomerType, CancellationToken ct = new CancellationToken());
        Task<IList<CustomerTypeTbl>> GetAllCustomerTypesAsync(CancellationToken ct = new CancellationToken());
        Task<CustomerTypeTbl> GetCustomerTypeAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}