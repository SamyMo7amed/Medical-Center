using System.ComponentModel.DataAnnotations;
namespace Medical_CenterAPI.Models
{
    public class Doctor:AppUser
    {
        public string Specialization { get; set; }
    }

}

