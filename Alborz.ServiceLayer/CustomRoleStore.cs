﻿using System.Data.Entity;
using Sabz.DomainClasses.DTO;
using Alborz.ServiceLayer.IService;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabz.DataLayer.Context;

namespace Alborz.ServiceLayer
{
    public class CustomRoleStore :
        RoleStore<CustomRole, int, CustomUserRole>,
        ICustomRoleStore
    {
        private readonly IUnitOfWork _context;

        public CustomRoleStore(IUnitOfWork context)
            : base((DbContext)context)
        {
            _context = context;
        }
    }
}