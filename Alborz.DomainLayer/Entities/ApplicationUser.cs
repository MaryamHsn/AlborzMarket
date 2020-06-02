using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace  Alborz.DomainLayer.Entities
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        // Add other properties here
        public string PurePassword { get; set; }
        //[ForeignKey("AddressId")]
        //public virtual Address Address { get; set; }
        //public int? AddressId { get; set; }
    }
}