using System.ComponentModel.DataAnnotations;
namespace Medical_CenterAPI.Models
{
    public class Assistant : AppUser
    {
        //properties that define relationShips
        public virtual List<AppointmentConfirmation>? AppointmentConfirmations { get; set; }

    }

}