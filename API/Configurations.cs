namespace API;
public static class Configurations
{
    public static Smtp SMTP;
    public static Mail MAIL;

    public class Smtp
    {
        public Smtp(string host, int port, string userName, string password)
        {
            Host = host;
            Port = port;
            UserName = userName;
            Password = password;
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class Mail
    {
        public Mail(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; set; }
        public string Name { get; set; }
    }
    public static class EmailMessages
    {
        public static string SendPasswordBody = "Here is your new Password: ";
        public static string SendPasswordSubject = "You Changed your password successfuly!!!";
    }
}