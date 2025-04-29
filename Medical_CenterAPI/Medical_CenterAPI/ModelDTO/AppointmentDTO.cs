using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.ModelDTO
{
    public class AppointmentDTO
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid? AssistantId { get; set; }
        public Guid? AppointmentConfirmationId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Appointment_Status Status { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
