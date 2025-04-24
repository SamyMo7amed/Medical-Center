using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical_CenterAPI.Models
{
    public enum Appointment_Status
    {
        Pending,
        Confirmed,
        Canceled
    }
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; set; }

        
        // 1-patiant

        public Guid PatiantId { get; set; }
        [ForeignKey(nameof(PatiantId))]
        public virtual Patient Patiant { get; set; }

        // 2-doctor

        public Guid DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor Doctor { get; set; }

        // 3-Assistant

        public Guid AssistantId { get; set; }
        [ForeignKey(nameof(AssistantId))]
        public virtual Assistant Assistant { get; set; }

        // 4- data 

        public DateTime AppointmentDate { get; set; }

        // 5-status

        public Appointment_Status Status { get; set; }

        // 6-CreatedAt

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //properties that define relationShips
        public virtual AppointmentConfirmation AppointmentConfirmation { get; set; }

        public Guid AppointmentConfirmationId { get; set; }  


    }
}