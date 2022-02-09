using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Commands.Visitant;
public class ChangeNameVisitantCommand : Notifiable<Notification>, ICommand
{
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
            .IsGreaterOrEqualsThan(FirstName.Length, 3, FirstName)
            .IsGreaterOrEqualsThan(LastName.Length, 3, LastName)
            .IsNullOrWhiteSpace(FirstName, FirstName)
            .IsNullOrWhiteSpace(LastName, LastName)
            );
    }
}