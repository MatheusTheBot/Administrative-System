using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Administrator;
public class ChangePasswordAdministratorCommand : Notifiable<Notification>, ICommand
{
    public ChangePasswordAdministratorCommand(string newPassword, string password, Guid id)
    {
        NewPassword = newPassword;
        Password = password;
        Id = id;
    }

    public string NewPassword { get; set; }
    public string Password { get; set; }
    public Guid Id { get; set; }

    public void Validate()
    {
        //TODO
    }
}