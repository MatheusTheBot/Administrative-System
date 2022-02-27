using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Login;
public class LoginCommand : Notifiable<Notification>, ICommand
{
    public LoginCommand(Guid id, string password, string role)
    {
        Id = id;
        Password = password;
        Role = role;
    }

    public Guid Id { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public void Validate()
    {
        if (Id == Guid.Empty)
            AddNotification(new Notification("Id", "Id can't be empty"));
        //TODO
    }
}