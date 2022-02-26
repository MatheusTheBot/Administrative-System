using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domain.Commands.Visitant;
public class ChangeNameVisitantCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public ChangeNameVisitantCommand(string firstName, string lastName, Guid id)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;

        Validate();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid Id { get; set; }
    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(FirstName, FirstName)
            .IsNotNullOrWhiteSpace(LastName, LastName)
            .IsGreaterOrEqualsThan(FirstName.Length, 3, FirstName)
            .IsGreaterOrEqualsThan(LastName.Length, 3, LastName)
            .IsFalse(Equals(Id, Guid.Empty), "Id")
            );
    }
}