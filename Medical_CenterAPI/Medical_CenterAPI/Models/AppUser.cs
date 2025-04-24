using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Medical_CenterAPI.Models
{
    public class AppUser :IdentityUser<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public  bool IsEmailConfirmed { get; set; } 

        public string Password {  get; set; }
        [Compare("Password")]   

        public string ConfirmPassword { get; set; } 
        public string Phone { get; set; }   
      
    }
}
