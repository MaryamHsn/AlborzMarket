using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface ICustomerService
    {
        void AddNewCustomer(CustomerTbl Customer);
        IList<CustomerTbl> GetAllCustomers();
        CustomerTbl GetCustomer(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewCustomerAsync(CustomerTbl Customer, CancellationToken ct = new CancellationToken());
        Task<IList<CustomerTbl>> GetAllCustomersAsync(CancellationToken ct = new CancellationToken());
        Task<CustomerTbl> GetCustomerAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}