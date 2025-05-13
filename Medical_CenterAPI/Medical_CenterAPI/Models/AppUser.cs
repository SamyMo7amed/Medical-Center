using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Medical_CenterAPI.Models
{
    public class AppUser :IdentityUser<Guid>
    {
     
        
   
        public string Password {  get; set; }
        [Compare("Password")]   

        public string ConfirmPassword { get; set; }
        public string? ConfirmToken { get; set; }    
        

      


    }
}
