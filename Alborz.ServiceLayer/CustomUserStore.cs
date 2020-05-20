using Sabz.DomainClasses.DTO;
using Alborz.ServiceLayer.IService;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabz.DataLayer.Context;

namespace Alborz.ServiceLayer
{
    public class CustomUserStore :
        UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        ICustomUserStore
    {
        private readonly IUnitOfWork _context;

        public CustomUserStore(IUnitOfWork context)
            : base((ApplicationDbContext)context)
        {
            _context = context;
        }

    }
}