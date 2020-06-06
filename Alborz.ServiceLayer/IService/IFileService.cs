using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{
    public interface IFileService
    {
        void AddNewImage(ImageTbl Image);
        IList<ImageTbl> GetAllImages();
        ImageTbl GetImage(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewImageAsync(ImageTbl Image, CancellationToken ct = new CancellationToken());
        Task<IList<ImageTbl>> GetAllImagesAsync(CancellationToken ct = new CancellationToken());
        Task<ImageTbl> GetImageAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}
