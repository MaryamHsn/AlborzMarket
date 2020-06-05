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
    public class ColorService : IColorService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public ColorService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public async Task<ColorDTO> AddNewColorAsync(ColorDTO Color, CancellationToken ct = new CancellationToken())
        {
            try
            {
                if (Color == null)
                    throw new ArgumentNullException();
                var entity = BaseMapper<ColorDTO, ColorTbl>.Map(Color);
                var obj = await _uow.ColorRepository.AddAsync(entity, ct);
                _uow.SaveAllChanges();
                var element = BaseMapper<ColorDTO, ColorTbl>.Map(obj); 
                return element;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
        public async Task<List<ColorDTO>> GetAllCategoriesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ColorRepository.GetAllAsync(ct);
            var entity = new List<ColorDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<ColorDTO, ColorTbl>.Map(item);
                  entity.Add(element);
            }
            return entity;
        }
        public async Task<List<ColorDTO>> GetCategoriesBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken())
        {
            var color = await GetAllCategoriesAsync();
            return color.Where(s => s.Title.Contains(searchItem)).ToList();
        }
        public async Task<ColorDTO> GetColorAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ColorRepository.GetAllAsync(x => x.Id == id);
            var element = BaseMapper<ColorDTO, ColorTbl>.Map(obj.FirstOrDefault());
            return element;
        }
        public async Task<ColorDTO> UpdateColorAsync(ColorDTO entity)
        {
            var obj = BaseMapper<ColorDTO, ColorTbl>.Map(entity);
            obj.IsActive = true;
            obj = await _uow.ColorRepository.UpdateAsync(obj);
            _uow.SaveAllChanges();
            var element = BaseMapper<ColorDTO, ColorTbl>.Map(obj); 
            return element;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Color = await _uow.ColorRepository.GetAsync(id, ct);
            var obj = await _uow.ColorRepository.SoftDeleteAsync(Color);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
