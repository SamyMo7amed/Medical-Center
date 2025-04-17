using System.ComponentModel.DataAnnotations;
namespace Medical_CenterAPI.Models
{
    public class Patiant:AppUser
    {
    


        [Range(0.0,150.0)]

        public double Age { get; set; }    
        public Dictionary<string, string>? MedicalHistory { get; set; }

    }
}
