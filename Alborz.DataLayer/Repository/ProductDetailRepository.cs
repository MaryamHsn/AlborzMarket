using Alborz.DataLayer.Context;
using Alborz.DataLayer.IRepository;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DataLayer.Repository
{ 
    public class ProductDetailRepository : BaseRepository<ProductDetailTbl, int>, IProductDetailRepository
    {
        public ProductDetailRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
