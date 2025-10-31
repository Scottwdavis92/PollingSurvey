using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PollingApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Employee Employee { get; set; }
    }
}
