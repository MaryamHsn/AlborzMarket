using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.Mapper;

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
        public async Task<PropertyValueDTO> AddNewPropertyValueAsync(PropertyValueDTO PropertyValue, CancellationToken ct = new CancellationToken())
        {
            if (PropertyValue == null)
                throw new ArgumentNullException();
            var entity = BaseMapper<PropertyValueDTO, PropertyValueTbl>.Map(PropertyValue);
            var obj = await _uow.PropertyValueRepository.AddAsync(entity, ct);
            _uow.SaveAllChanges();
            var element = BaseMapper<PropertyValueDTO, PropertyValueTbl>.Map(obj);
            return element;
        }
        public async Task<List<PropertyValueDTO>> GetAllPropertyValuesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyValueRepository.GetAllAsync(ct);
            var entity = new List<PropertyValueDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<PropertyValueDTO, PropertyValueTbl>.Map(item);
                entity.Add(element);
            }
            return entity;
        }
        public async Task<List<PropertyValueDTO>> GetPropertyValuesBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken())
        {
            var product = await GetAllPropertyValuesAsync();
            return product.Where(s => s.Value.Contains(searchItem) || s.Properties.Select(x => x.Title.Contains(searchItem)).FirstOrDefault()).ToList();
        }
        public async Task<PropertyValueDTO> GetPropertyValueAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyValueRepository.GetAllAsync(x => x.Id == id);
            var element = BaseMapper<PropertyValueDTO, PropertyValueTbl>.Map(obj.FirstOrDefault());
            return element;
        }
        public async Task<PropertyValueDTO> UpdatePropertyValueAsync(PropertyValueDTO entity)
        {
            var obj = BaseMapper<PropertyValueDTO, PropertyValueTbl>.Map(entity);
            obj.IsActive = true;
            obj = await _uow.PropertyValueRepository.UpdateAsync(obj);
            _uow.SaveAllChanges();
            var element = BaseMapper<PropertyValueDTO, PropertyValueTbl>.Map(obj);
            return element;
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
