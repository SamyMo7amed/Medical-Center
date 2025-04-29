using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.ModelDTO
{
    public class AppointmentConfirmationDTO
    {

        public Guid AppointmentId { get; set; }
        public virtual Assistant? Assistant { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid PatientId { get; set; }
     
        public DateTime ConfirmationDate { get; set; }
    }
}
