using Microsoft.AspNetCore.Identity;

namespace ForceTestingService.Infrastructure.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}