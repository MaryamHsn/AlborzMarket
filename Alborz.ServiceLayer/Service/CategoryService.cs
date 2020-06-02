using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;
using Alborz.ServiceLayer.Mapper;

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
        public void AddNewCategory(CategoryTbl Category)
        {
            _uow.CategoryRepository.Add(Category);
            _uow.SaveAllChanges();
        }
        public IList<CategoryTbl> GetAllCategories()
        {
            return _uow.CategoryRepository.GetAll().ToList();
        }
        public CategoryTbl GetCategory(int? id)
        {
            return _uow.CategoryRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public CategoryTbl UpdateCategory(CategoryTbl entity)
        {
            entity = _uow.CategoryRepository.Update(entity);
            _uow.SaveAllChanges();
            return entity;
        }
        public bool Delete(int id)
        {
            CategoryTbl Category = _uow.CategoryRepository.Get(id);
            var t = _uow.CategoryRepository.SoftDelete(Category);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewCategoryAsync(CategoryTbl Category, CancellationToken ct = new CancellationToken())
        {
            await _uow.CategoryRepository.AddAsync(Category, ct);
            _uow.SaveAllChanges();
        }
        public async Task<List<CategoryTbl>> GetAllCategoriesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CategoryRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<CategoryTbl> GetCategoryAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.CategoryRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        //public async Task<CategoryViewModel> GetCategoryAsync(int? id, CancellationToken ct = new CancellationToken())
        //{
        //    var category = await _uow.CategoryRepository.GetAllAsync(x => x.Id == id);
        //    var obj = BaseMapper<CategoryViewModel, CategoryTbl>.Map(category.FirstOrDefault());
        //    return obj;
        //}
        public async Task<CategoryTbl> UpdateCategoryAsync(CategoryTbl entity)
        {
            entity = await _uow.CategoryRepository.UpdateAsync(entity);
            _uow.SaveAllChanges();
            return entity;
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
