using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Medical_CenterAPI.Models;
using System.Text.Json.Serialization;

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

    public Guid? PatientId { get; set; }


    [JsonIgnore]
    [ForeignKey(nameof(PatientId))]
    public virtual Patient? Patiant { get; set; }

    // 2-doctor

    public Guid? DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Doctor Doctor { get; set; }

    // 3-Assistant

    public Guid? AssistantId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
     [ForeignKey(nameof(AssistantId))]
    public virtual Assistant? Assistant { get; set; }

    // 4- data 

    public DateTime AppointmentDate { get; set; }

    // 5-status

    public Appointment_Status Status { get; set; }

    // 6-CreatedAt

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //properties that define relationShips
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual AppointmentConfirmation? AppointmentConfirmation { get; set; }

    public Guid? AppointmentConfirmationId { get; set; }  


}