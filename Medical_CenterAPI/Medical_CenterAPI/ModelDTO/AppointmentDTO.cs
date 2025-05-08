using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.ModelDTO
{
    public class AppointmentDTO
    {
      
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
  
      
        public DateTime Created { get; set; }= DateTime.Now;
        public DateTime AppointmentDate { get; set; }


    }
}
