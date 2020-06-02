using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{ 
    public class PropertyService : IPropertyService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public PropertyService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewProperty(PropertyTbl Property)
        {
            _uow.PropertyRepository.Add(Property);
            _uow.SaveAllChanges();
        }
        public IList<PropertyTbl> GetAllProperties()
        {
            return _uow.PropertyRepository.GetAll().ToList();
        }
        public PropertyTbl GetProperty(int? id)
        {
            return _uow.PropertyRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            PropertyTbl Property = _uow.PropertyRepository.Get(id);
            var t = _uow.PropertyRepository.SoftDelete(Property);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewPropertyAsync(PropertyTbl Property, CancellationToken ct = new CancellationToken())
        {
            await _uow.PropertyRepository.AddAsync(Property, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<PropertyTbl>> GetAllPropertiesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<PropertyTbl> GetPropertyAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Property = await _uow.PropertyRepository.GetAsync(id, ct);
            var obj = await _uow.PropertyRepository.SoftDeleteAsync(Property);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
