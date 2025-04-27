namespace Medical_CenterAPI.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
      

    }
}
