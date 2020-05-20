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
    public class PropertyValueRepository : BaseRepository<PropertyValueTbl, int>, IPropertyValueRepository
    {
        public PropertyValueRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
