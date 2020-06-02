using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{
    public interface ICategoryService
    {

        Task<CategoryDTO> AddNewCategoryAsync(CategoryDTO Category, CancellationToken ct = new CancellationToken());
        Task<List<CategoryDTO>> GetAllCategoriesAsync(CancellationToken ct = new CancellationToken());
        Task<List<CategoryDTO>> GetCategoriesBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken());
        Task<CategoryDTO> GetCategoryAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO entity);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}