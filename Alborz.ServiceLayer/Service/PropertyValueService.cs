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
    public class PropertyValueService : IPropertyValueService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public PropertyValueService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewPropertyValue(PropertyValueTbl PropertyValue)
        {
            _uow.PropertyValueRepository.Add(PropertyValue);
            _uow.SaveAllChanges();
        }
        public IList<PropertyValueTbl> GetAllPropertyValues()
        {
            return _uow.PropertyValueRepository.GetAll().ToList();
        }
        public PropertyValueTbl GetPropertyValue(int? id)
        {
            return _uow.PropertyValueRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            PropertyValueTbl PropertyValue = _uow.PropertyValueRepository.Get(id);
            var t = _uow.PropertyValueRepository.SoftDelete(PropertyValue);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewPropertyValueAsync(PropertyValueTbl PropertyValue, CancellationToken ct = new CancellationToken())
        {
            await _uow.PropertyValueRepository.AddAsync(PropertyValue, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<PropertyValueTbl>> GetAllPropertyValuesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyValueRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<PropertyValueTbl> GetPropertyValueAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyValueRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var PropertyValue = await _uow.PropertyValueRepository.GetAsync(id, ct);
            var obj = await _uow.PropertyValueRepository.SoftDeleteAsync(PropertyValue);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
