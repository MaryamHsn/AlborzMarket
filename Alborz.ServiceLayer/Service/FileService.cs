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
    public class FileService : IFileService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public FileService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public async Task<FileDTO> AddNewFileAsync(FileDTO file, CancellationToken ct = new CancellationToken())
        {
            if (file == null)
                throw new ArgumentNullException();
            var entity = BaseMapper<FileDTO, FileTbl>.Map(file);
            var obj = await _uow.FileRepository.AddAsync(entity, ct);
            _uow.SaveAllChanges();
            var element = BaseMapper<FileDTO, FileTbl>.Map(obj);
            return element;
        }
        public async Task<List<FileDTO>> GetAllFilesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.FileRepository.GetAllAsync(ct);
            var entity = new List<FileDTO>();
            foreach (var item in obj)
            {
                var element = BaseMapper<FileDTO, FileTbl>.Map(item);
            }
            return entity;
        }
        public async Task<List<FileDTO>> GetFilesBySearchItemAsync(string searchItem, CancellationToken ct = new CancellationToken())
        {
            var file = await GetAllFilesAsync();
            return file.Where(s => s.Title.Contains(searchItem)).ToList();
        }
        public async Task<FileDTO> GetFileAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.FileRepository.GetAllAsync(x => x.Id == id);
            var element = BaseMapper<FileDTO, FileTbl>.Map(obj.FirstOrDefault());
            return element;
        }
        public async Task<FileDTO> UpdateFileAsync(FileDTO entity)
        {
            var obj = BaseMapper<FileDTO, FileTbl>.Map(entity);
            obj.IsActive = true;
            obj = await _uow.FileRepository.UpdateAsync(obj);
            _uow.SaveAllChanges();
            var element = BaseMapper<FileDTO, FileTbl>.Map(obj);
            return element;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var File = await _uow.FileRepository.GetAsync(id, ct);
            var obj = await _uow.FileRepository.SoftDeleteAsync(File);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
