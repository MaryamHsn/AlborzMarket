using Microsoft.AspNet.Identity.EntityFramework;

namespace  Alborz.DomainLayer.DTO
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
}