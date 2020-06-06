using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
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
        public void AddNewImage(ImageTbl Image)
        {
            _uow.ImageRepository.Add(Image);
            _uow.SaveAllChanges();
        }
        public IList<ImageTbl> GetAllImages()
        {
            return _uow.ImageRepository.GetAll().ToList();
        }
        public ImageTbl GetImage(int? id)
        {
            return _uow.ImageRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            ImageTbl Image = _uow.ImageRepository.Get(id);
            var t = _uow.ImageRepository.SoftDelete(Image);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewImageAsync(ImageTbl Image, CancellationToken ct = new CancellationToken())
        {
            await _uow.ImageRepository.AddAsync(Image, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<ImageTbl>> GetAllImagesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ImageRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<ImageTbl> GetImageAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ImageRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Image = await _uow.ImageRepository.GetAsync(id, ct);
            var obj = await _uow.ImageRepository.SoftDeleteAsync(Image);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
