using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;
using Alborz.ServiceLayer.Mapper;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.Utils;

namespace Alborz.ServiceLayer.Service
{ 
    public class CategoryService : ICategoryService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public CategoryService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public async Task<CategoryDTO> AddNewCategoryAsync(CategoryDTO Category, CancellationToken ct = new CancellationToken())
        {
            if (Category == null)
                throw new ArgumentNullException();
            var entity = BaseMapper<CategoryDTO, CategoryTbl>.Map(Category);          
            var obj= await _uow.CategoryRepository.AddAsync(entity, ct); 
            _uow.SaveAllChanges();
            var element = BaseMapper<CategoryDTO, CategoryTbl>.Map(obj);
            element.StartDateString = ((DateTime)(obj.StartDate)).ToPersianDateString();
            element.EndDateString = ((DateTime)(obj.EndDate)).ToPersianDateString();
            return element; 
        }
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CategoryRepository.GetAllAsync(ct);
            var entity = new List<CategoryDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<CategoryDTO, CategoryTbl>.Map(item);
                element.StartDateString = ((DateTime)(item.StartDate)).ToPersianDateString();
                element.EndDateString = ((DateTime)(item.EndDate)).ToPersianDateString();
                entity.Add(element);
            }
            return entity; 
        }
        public async Task<List<CategoryDTO>> GetCategoriesBySearchItemAsync(string searchItem,CancellationToken ct = new CancellationToken())
        {
            var category =await GetAllCategoriesAsync();
            return category.Where(s => s.Title.Contains(searchItem)|| s.Code.Contains(searchItem)|| s.priority.ToString().Contains(searchItem)).ToList();
        }
        public async Task<CategoryDTO> GetCategoryAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CategoryRepository.GetAllAsync(x => x.Id == id);
            var element = BaseMapper<CategoryDTO, CategoryTbl>.Map(obj.FirstOrDefault());
            element.StartDateString = ((DateTime)(obj.FirstOrDefault().StartDate)).ToPersianDateString();
            element.EndDateString = ((DateTime)(obj.FirstOrDefault().EndDate)).ToPersianDateString();
            return element;
        }
        public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO entity)
        {
            var obj = BaseMapper<CategoryDTO, CategoryTbl>.Map(entity);
            obj = await _uow.CategoryRepository.UpdateAsync(obj);
            obj.IsActive = true;
            _uow.SaveAllChanges();
            var element = BaseMapper<CategoryDTO, CategoryTbl>.Map(obj);
            element.StartDateString = ((DateTime)(obj.StartDate)).ToPersianDateString();
            element.EndDateString = ((DateTime)(obj.EndDate)).ToPersianDateString();
            return element; 
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Category = await _uow.CategoryRepository.GetAsync(id, ct);
            var obj = await _uow.CategoryRepository.SoftDeleteAsync(Category);
            _uow.SaveAllChanges();
            return obj;
        }
    
    }
}
