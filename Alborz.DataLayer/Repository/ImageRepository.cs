using Alborz.DataLayer.Context;
using Alborz.DataLayer.IRepository;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DataLayer.Repository
{
    public class ImageRepository : BaseRepository<ImageTbl, int>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    } 
}
