using System.ComponentModel.DataAnnotations;
namespace Medical_CenterAPI.Models
{
    public class Assistant : AppUser
    {







        //properties that define relationShips
        public List<AppointmentConfirmation> AppointmentConfirmations { get; set; }

    }

}