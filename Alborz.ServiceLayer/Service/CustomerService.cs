using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;

namespace Alborz.ServiceLayer.Service
{ 
    public class CustomerService : ICustomerService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public CustomerService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewCustomer(CustomerTbl Customer)
        {
            _uow.CustomerRepository.Add(Customer);
            _uow.SaveAllChanges();
        }
        public IList<CustomerTbl> GetAllCustomers()
        {
            return _uow.CustomerRepository.GetAll().ToList();
        }
        public CustomerTbl GetCustomer(int? id)
        {
            return _uow.CustomerRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            CustomerTbl Customer = _uow.CustomerRepository.Get(id);
            var t = _uow.CustomerRepository.SoftDelete(Customer);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewCustomerAsync(CustomerTbl Customer, CancellationToken ct = new CancellationToken())
        {
            await _uow.CustomerRepository.AddAsync(Customer, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<CustomerTbl>> GetAllCustomersAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CustomerRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<CustomerTbl> GetCustomerAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CustomerRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Customer = await _uow.CustomerRepository.GetAsync(id, ct);
            var obj = await _uow.CustomerRepository.SoftDeleteAsync(Customer);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
