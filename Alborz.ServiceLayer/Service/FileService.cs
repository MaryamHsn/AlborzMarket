using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using Alborz.ServiceLayer.Mapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

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
        public async Task<FileDTO> GetFileByGuidAsync(Guid id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.FileRepository.GetAllAsync(x => x.IdFile == id);
            var element = BaseMapper<FileDTO, FileTbl>.Map(obj.FirstOrDefault());
            return element;
        }
        public async Task<List<FileDTO>> GetFilesByEntityEnumKeysAsync(int entityEnumId, int entityKeyId, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.FileRepository.GetAllAsync(x => x.EntityEnumId == entityEnumId && x.EntityKeyId == entityKeyId);
            //var element = BaseMapper<FileDTO, FileTbl>.Map(obj.FirstOrDefault());
            var element = obj.Select(BaseMapper<FileDTO, FileTbl>.Map).Where(x => x.IsActive == true).ToList();
            //var remoteResponse = JsonConvert.DeserializeObject<ICollection<FileDTO>>(element.FirstOrDefault().ContentType);

            return element;
        } 
        //public async Task<List<FileDTO>> ShowDocumentFileData( int entityId,int keyId)
        //{
        //    var content = await _uow.FileRepository.GetAllAsync(x => x.EntityEnumId == entityId && x.EntityKeyId == keyId);
        //    var remoteResponse = JsonConvert.DeserializeObject<List<FileDTO>>(content.);
        //}
        //public FileContentResult ShowDocument(int id)
        //{
        //    var document = DocumentFileDataService.Instance().ShowDocumentFileData((int)DocumentEntityEnum.Agent, id).FirstOrDefault();
        //    var imagedata = document.FileBytes;

        //    return File(imagedata, "image/jpg");
        //}
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
        public byte[] ConvertHttpPostedFileBaseToByte(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                byte[] data;
                using (Stream inputStream = file.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }

                    data = memoryStream.ToArray();
                    return data;
                }
            }
            return null;
        }

    }
}
