using System.ComponentModel.DataAnnotations;

namespace Medical_CenterAPI.ModelDTO
{
    public class EmployeeDTO:RegisterUser
    {
        public string? Specialization { get; set; }

       

    }
}
