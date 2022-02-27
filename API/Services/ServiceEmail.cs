using System.Net.Mail;

namespace API.Services;
public class ServiceEmail
{
    public bool SendEmail(string fromSomeone, string fromEmail, string toSomeone, string toEmail, string subject, string body)
    {
        var smtp = new SmtpClient();
        return false;
    }
}