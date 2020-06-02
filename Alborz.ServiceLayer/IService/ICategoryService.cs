using Alborz.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface ICategoryService
    {
        void AddNewCategory(CategoryTbl Category);
        IList<CategoryTbl> GetAllCategories();
        CategoryTbl GetCategory(int? id);
        CategoryTbl UpdateCategory(CategoryTbl entity);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewCategoryAsync(CategoryTbl Category, CancellationToken ct = new CancellationToken());
        Task<List<CategoryTbl>> GetAllCategoriesAsync(CancellationToken ct = new CancellationToken());
        //Task<CategoryViewModel> GetCategoryAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<CategoryTbl> GetCategoryAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<CategoryTbl> UpdateCategoryAsync(CategoryTbl entity);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}