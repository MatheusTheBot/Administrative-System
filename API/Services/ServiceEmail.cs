using System.Net;
using System.Net.Mail;

namespace API.Services;
public class ServiceEmail
{
    public bool SendEmail(
        string toName,
        string toEmail,
        string subject,
        string body)
    {
        var smtpClient = new SmtpClient(Configurations.SMTP.Host, Configurations.SMTP.Port);

        smtpClient.Credentials = new NetworkCredential(Configurations.SMTP.UserName, Configurations.SMTP.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
        var mail = new MailMessage();

        mail.From = new MailAddress(Configurations.MAIL.Email, Configurations.MAIL.Name);
        mail.To.Add(new MailAddress(toEmail, toName));
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        try
        {
            smtpClient.Send(mail);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}