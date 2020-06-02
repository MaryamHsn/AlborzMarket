using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alborz.DataLayer.IRepository;
using System.Threading.Tasks;

namespace Alborz.DataLayer.Repository
{ 
    public class CartItemRepository : BaseRepository<CartItemTbl, int>, ICartItemRepository
    {
        public CartItemRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
