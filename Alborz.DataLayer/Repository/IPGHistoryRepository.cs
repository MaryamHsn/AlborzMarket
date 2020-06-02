﻿using Alborz.DataLayer.Context;
using Alborz.DataLayer.IRepository;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DataLayer.Repository
{
    public class IPGHistoryRepository : BaseRepository<IPGHistoryTbl, int>, IIPGHistoryRepository
    {
        public IPGHistoryRepository(ApplicationDbContext contextFactory) : base(contextFactory)
        {
        }
    }
}
