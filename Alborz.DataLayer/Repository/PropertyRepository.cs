﻿using Alborz.DataLayer.Context;
using Alborz.DataLayer.IRepository;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DataLayer.Repository
{ 
    public class  PropertyRepository : BaseRepository< PropertyTbl, int>, IPropertyRepository
    {
        public  PropertyRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}