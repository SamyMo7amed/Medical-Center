using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
namespace Medical_CenterAPI.Models
{
    public class Patient:AppUser
    {
    


        [Range(0.0,150.0)]

        public double Age { get; set; }

        public string MedicalHistoryJson { get; set; }

        [NotMapped]
        public Dictionary<string, string>? MedicalHistory 
        { get => string.IsNullOrEmpty(MedicalHistoryJson) ? new Dictionary<string, string>() : JsonSerializer.Deserialize<Dictionary<string, string>>(MedicalHistoryJson);
            set => MedicalHistoryJson = JsonSerializer.Serialize(value); }

        //properties that define relationShips
        public virtual List<Appointment> Appointments { get; set; } 
        

    }
}
