using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Medical_CenterAPI.Models
{

    public enum ConfirmationStatus
    {
        Success, Failure        
    }
    public class AppointmentConfirmation
    {




//ConfirmationDate

//CreatedAt

//UpdatedAt
         public Guid Id { get; set; }    
         public  Guid? AppointmentId { get; set; }
        [ForeignKey(nameof(AppointmentId))]
        [JsonIgnore]

        public virtual Appointment? Appointment { get; set; }

        public Guid? AssistantId { get; set; }
        [ForeignKey(nameof(AssistantId))]
        [JsonIgnore]

        public virtual Assistant? Assistant { get; set; }

        public DateTime ConfirmationDate { get; set; }
          [ForeignKey(nameof(patientId))]
        [JsonIgnore]
        public virtual Patient? Patient { get; set; }

        
        public Guid? patientId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
