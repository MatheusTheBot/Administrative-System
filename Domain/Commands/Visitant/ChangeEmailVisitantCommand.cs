using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domain.Commands.Visitant;
public class ChangeEmailVisitantCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public ChangeEmailVisitantCommand(string email, Guid id)
    {
        Email = email;
        Id = id;

        Validate();
    }

    public string Email { get; set; }
    public Guid Id { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Email, "Email")
            .IsEmail(Email, "Email")
            .AreNotEquals(Id, Guid.Empty, "Id")
        );
    }
}