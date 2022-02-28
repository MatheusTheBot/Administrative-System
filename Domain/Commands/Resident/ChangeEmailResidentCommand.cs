using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domain.Commands.Resident;
public class ChangeEmailResidentCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public ChangeEmailResidentCommand(string email, Guid id, int number = 0, int block = 0)
    {
        Email = email;
        Id = id;
        Number = number;
        Block = block;

        Validate();
    }

    public string Email { get; set; }
    public Guid Id { get; set; }
    public int Number { get; set; }
    public int Block { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
        .Requires()
        .IsFalse(Equals(Id, Guid.Empty), "Id")
        .IsNotNullOrWhiteSpace(Email, Email)
        .IsEmail(Email, Email)
        );
    }
}