using System.ComponentModel.DataAnnotations;

namespace Medical_CenterAPI.ModelDTO
{
    public class ConfirmDTO
    {


        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }







    }
}
