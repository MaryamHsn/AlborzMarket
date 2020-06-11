﻿using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DataLayer.IRepository
{
    public interface IColorRepository : IRepository<ColorTbl, int>
    {
    } 
}
