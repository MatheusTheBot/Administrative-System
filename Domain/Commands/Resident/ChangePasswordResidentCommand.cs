using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Commands.Resident;
public class ChangePasswordResidentCommand : Notifiable<Notification>, ICommand
{
    public ChangePasswordResidentCommand(string newPassword, string password, Guid id)
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
            .AreNotEquals(Id, Guid.Empty, "Id")
            .IsNotNullOrWhiteSpace(Password, "Password")
            .IsBetween(Password.Length, 8, 50, "Password")
            .IsNotNullOrWhiteSpace(NewPassword, "New Password")
            .IsBetween(NewPassword.Length, 8, 50, "New Password")
        );
    }
}