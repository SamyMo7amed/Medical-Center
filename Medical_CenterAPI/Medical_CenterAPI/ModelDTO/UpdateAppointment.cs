namespace Medical_CenterAPI.ModelDTO
{
    public class UpdateAppointment
    {
        public Guid AppointmentId {  get; set; }    
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid AssistantId { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime AppointmentDate { get; set; }
    }
}
