using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{ 
    public class AddressService : IAddressService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public AddressService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewAddress(AddressTbl Address)
        {
            _uow.AddressRepository.Add(Address);
            _uow.SaveAllChanges();
        }
        public IList<AddressTbl> GetAllAddresss()
        {
            return _uow.AddressRepository.GetAll().ToList();
        }
        public AddressTbl GetAddress(int? id)
        {
            return _uow.AddressRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            AddressTbl Address = _uow.AddressRepository.Get(id);
            var t = _uow.AddressRepository.SoftDelete(Address);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewAddressAsync(AddressTbl Address, CancellationToken ct = new CancellationToken())
        {
            await _uow.AddressRepository.AddAsync(Address, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<AddressTbl>> GetAllAddresssAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.AddressRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<AddressTbl> GetAddressAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.AddressRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Address = await _uow.AddressRepository.GetAsync(id, ct);
            var obj = await _uow.AddressRepository.SoftDeleteAsync(Address);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
}
