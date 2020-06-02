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
    public class CustomerTypeService : ICustomerTypeService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public CustomerTypeService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewCustomerType(CustomerTypeTbl CustomerType)
        {
            _uow.CustomerTypeRepository.Add(CustomerType);
            _uow.SaveAllChanges();
        }
        public IList<CustomerTypeTbl> GetAllCustomerTypes()
        {
            return _uow.CustomerTypeRepository.GetAll().ToList();
        }
        public CustomerTypeTbl GetCustomerType(int? id)
        {
            return _uow.CustomerTypeRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            CustomerTypeTbl CustomerType = _uow.CustomerTypeRepository.Get(id);
            var t = _uow.CustomerTypeRepository.SoftDelete(CustomerType);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewCustomerTypeAsync(CustomerTypeTbl CustomerType, CancellationToken ct = new CancellationToken())
        {
            await _uow.CustomerTypeRepository.AddAsync(CustomerType, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<CustomerTypeTbl>> GetAllCustomerTypesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CustomerTypeRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<CustomerTypeTbl> GetCustomerTypeAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CustomerTypeRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var CustomerType = await _uow.CustomerTypeRepository.GetAsync(id, ct);
            var obj = await _uow.CustomerTypeRepository.SoftDeleteAsync(CustomerType);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
