using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Commands.Resident;
public class ChangeEmailResidentCommand : Notifiable<Notification>, ICommand
{
    public ChangeEmailResidentCommand(string email, Guid id)
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
        .IsNotNullOrWhiteSpace(Email, Email)
        .IsEmail(Email, Email)
        );
    }
}