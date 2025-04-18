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
        
        public Appointment Appointment { get; set; }

        public Guid AssistantId { get; set; }   
        public Assistant Assistant { get; set; }

        public DateTime ConfirmationDate { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
