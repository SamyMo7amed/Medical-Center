using System.ComponentModel.DataAnnotations.Schema;

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
         public  Guid AppointmentId { get; set; }
        [ForeignKey(nameof(AppointmentId))] 
        
        public Appointment Appointment { get; set; }

        public Guid AssistantId { get; set; }
        [ForeignKey(nameof(AssistantId))]      
        
        public Assistant Assistant { get; set; }

        public DateTime ConfirmationDate { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
