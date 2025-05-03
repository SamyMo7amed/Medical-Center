namespace Medical_CenterAPI.ModelDTO
{
    /// <summary>
    ///  it  is for Account Controller
    ///  
    /// </summary>
    public class UpdateDTO
    {

        public string? Specialization { get; set; }
        public string UserName { get; set; }

        public double? Age
            { get; set; }   
        public string PhoneNumber { get; set; }
        public IFormFile? image { get; set; }
    }
}
