using System.Data.Entity;
using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using Microsoft.AspNet.Identity.EntityFramework; 

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