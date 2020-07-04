using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Alborz.ServiceLayer.IService
{
    public interface IFileService
    {
        Task<FileDTO> AddNewFileAsync(FileDTO file, CancellationToken ct = new CancellationToken());
        Task<List<FileDTO>> GetAllFilesAsync(CancellationToken ct = new CancellationToken());
        Task<List<FileDTO>> GetFilesBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken());
        Task<FileDTO> GetFileAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<FileDTO> GetFileByGuidAsync(Guid id, CancellationToken ct = new CancellationToken());
        Task<List<FileDTO>> GetFilesByEntityEnumKeysAsync(int entityEnumId, int entityKeyId, CancellationToken ct = new CancellationToken());
        Task<FileDTO> UpdateFileAsync(FileDTO entity);
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
        byte[] ConvertHttpPostedFileBaseToByte(HttpPostedFileBase file);
    }
}
