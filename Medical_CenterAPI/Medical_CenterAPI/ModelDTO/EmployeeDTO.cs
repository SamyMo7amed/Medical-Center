using System.ComponentModel.DataAnnotations;

namespace Medical_CenterAPI.ModelDTO
{
    public class EmployeeDTO:RegisterUser
    {
        public string? Specialization { get; set; }

        [Range(22.0,70.0)]
        public override double Age { get => base.Age; set => base.Age = value; }

    }
}
