using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Commands.Administrator;
public class ChangePasswordAdministratorCommand : Notifiable<Notification>, ICommand
{
    public ChangePasswordAdministratorCommand(string newPassword, string password, Guid id)
    {
        NewPassword = newPassword;
        Password = password;
        Id = id;

        Validate();
    }

    public string NewPassword { get; set; }
    public string Password { get; set; }
    public Guid Id { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Password, "Password")
            .IsNotNullOrWhiteSpace(NewPassword, "New Password")
            .IsBetween(Password.Length, 8, 50, "Password")
            .IsBetween(NewPassword.Length, 8, 50, "New Password")
            .AreNotEquals(Id, Guid.Empty, "Id")
        );
    }
}