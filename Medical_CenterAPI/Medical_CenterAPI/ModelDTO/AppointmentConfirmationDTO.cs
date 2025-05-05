using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.ModelDTO
{
    public class AppointmentConfirmationDTO
    {

        public Guid Id { get; set; }        
        public Guid AppointmentId { get; set; }
        public Guid? AssistantId { get; set; }  

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public Guid PatientId { get; set; }
     
        public DateTime ConfirmationDate { get; set; }
    }
}
