using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domain.Commands.Administrator;
public class GenerateNewPasswordAdminCommand : Notifiable<Notification>, ICommand
{
    public GenerateNewPasswordAdminCommand(string password, Guid id)
    {
        Password = password;
        Id = id;
    }

    [JsonIgnore]
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