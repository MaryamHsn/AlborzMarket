using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{
    public interface IColorService
    {
        Task<ColorDTO> AddNewColorAsync(ColorDTO Color, CancellationToken ct = new CancellationToken());
        Task<List<ColorDTO>> GetAllColorsAsync(CancellationToken ct = new CancellationToken());
        Task<List<ColorDTO>> GetColorsBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken());
        Task<ColorDTO> GetColorAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<ColorDTO> UpdateColorAsync(ColorDTO entity);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}
