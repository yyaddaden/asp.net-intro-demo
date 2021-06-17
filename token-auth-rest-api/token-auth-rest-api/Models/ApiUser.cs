using Microsoft.AspNetCore.Identity;

namespace token_auth_rest_api.Models
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
