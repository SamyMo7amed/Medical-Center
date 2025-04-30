using System.ComponentModel.DataAnnotations;
namespace Medical_CenterAPI.Models
{
    public class Doctor:AppUser
    {
        public string Specialization { get; set; }





        //properties that define relationShips

         public virtual List<Appointment>? Appointments { get; set; }

        
    }

}

