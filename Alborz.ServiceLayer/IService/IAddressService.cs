using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IAddressService
    {
        void AddNewAddress(AddressTbl Address);
        IList<AddressTbl> GetAllAddresss();
        AddressTbl GetAddress(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewAddressAsync(AddressTbl Address, CancellationToken ct = new CancellationToken());
        Task<IList<AddressTbl>> GetAllAddresssAsync(CancellationToken ct = new CancellationToken());
        Task<AddressTbl> GetAddressAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}