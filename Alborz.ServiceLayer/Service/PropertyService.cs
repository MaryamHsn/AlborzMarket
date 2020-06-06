using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using Alborz.ServiceLayer.Mapper;
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
        public async Task<PropertyDTO> AddNewPropertyAsync(PropertyDTO Property, CancellationToken ct = new CancellationToken())
        {
            if (Property == null)
                throw new ArgumentNullException();
            var entity = BaseMapper<PropertyDTO, PropertyTbl>.Map(Property);
            var obj = await _uow.PropertyRepository.AddAsync(entity, ct);
            _uow.SaveAllChanges();
            var element = BaseMapper<PropertyDTO, PropertyTbl>.Map(obj);
            return element;
        }
       public async Task AddAllPropertiesAsync(List<PropertyDTO> Properties, CancellationToken ct = new CancellationToken())
        {
            if (Properties == null)
                throw new ArgumentNullException();
            foreach (var item in Properties)
            {
                var entity = BaseMapper<PropertyDTO, PropertyTbl>.Map(item);
                await _uow.PropertyRepository.AddAsync(entity, ct);
            }
            _uow.SaveAllChanges();  
        }
        public async Task<List<PropertyDTO>> GetAllPropertysAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyRepository.GetAllAsync(ct);
            var entity = new List<PropertyDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<PropertyDTO, PropertyTbl>.Map(item);
                entity.Add(element);
            }
            return entity;
        }
        public async Task<List<PropertyDTO>> GetAllPropertiesByProductIdAsync(int? productId, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyRepository.GetAllAsync(x => x.productId == productId, ct);
            var entity = new List<PropertyDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<PropertyDTO, PropertyTbl>.Map(item);
                entity.Add(element);
            }
            return entity;
        }
        public async Task<List<PropertyDTO>> GetAllPropertiesByCategoryIdAsync(int categoryId, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyRepository.GetAllAsync(x => x.CategoryId == categoryId, ct);
            var entity = new List<PropertyDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<PropertyDTO, PropertyTbl>.Map(item);
                entity.Add(element);
            }
            return entity;
        }
        public async Task<List<PropertyDTO>> GetPropertysBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken())
        {
            var product = await GetAllPropertysAsync();
            return product.Where(s => s.Title.Contains(searchItem) || s.Title.Contains(searchItem) || s.Categories.Select(x => x.Title.Contains(searchItem)).FirstOrDefault() || s.Products.Select(x => x.Title.Contains(searchItem)).FirstOrDefault()).ToList();
        }
        public async Task<PropertyDTO> GetPropertyAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PropertyRepository.GetAllAsync(x => x.Id == id);
            var element = BaseMapper<PropertyDTO, PropertyTbl>.Map(obj.FirstOrDefault());
            return element;
        }
        public async Task<PropertyDTO> UpdatePropertyAsync(PropertyDTO entity)
        {
            var obj = BaseMapper<PropertyDTO, PropertyTbl>.Map(entity);
            obj.IsActive = true;
            obj = await _uow.PropertyRepository.UpdateAsync(obj);
            _uow.SaveAllChanges();
            var element = BaseMapper<PropertyDTO, PropertyTbl>.Map(obj);
            return element;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Property = await _uow.PropertyRepository.GetAsync(id, ct);
            var obj = await _uow.PropertyRepository.SoftDeleteAsync(Property);
            _uow.SaveAllChanges();
            return obj;
        }
        public async Task DeleteByProductIdAsync(int productId, CancellationToken ct = new CancellationToken())
        {
            var props = await _uow.PropertyRepository.GetAllAsync(x=>x.productId==productId, ct);
            foreach (var item in props)
            {
                await _uow.PropertyRepository.SoftDeleteAsync(item);
            }
            _uow.SaveAllChanges();
        }
        public async Task DeleteByCategoryIdAsync(int categoryId, CancellationToken ct = new CancellationToken())
        {
            var props = await _uow.PropertyRepository.GetAllAsync(x=>x.CategoryId==categoryId, ct);
            foreach (var item in props)
            {
                await _uow.PropertyRepository.SoftDeleteAsync(item);
            }
            _uow.SaveAllChanges();
        }
    }
}
