using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domain.Commands.Resident;
public class ChangeNameResidentCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public ChangeNameResidentCommand(string firstName, string lastName, Guid id, int number = 0, int block = 0)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
        Number = number;
        Block = block;

        Validate();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid Id { get; set; }
    public int Number { get; set; }
    public int Block { get; set; }
    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(FirstName, "FirstName")
            .IsNotNullOrWhiteSpace(LastName, "LastName")
            .IsGreaterOrEqualsThan(FirstName.Length, 3, "FirstName")
            .IsGreaterOrEqualsThan(LastName.Length, 3, "LastName")
            .AreNotEquals(Id, Guid.Empty,"Id")
        );
    }
}