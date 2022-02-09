using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Visitant;
public class ChangeActiveVisitantCommand : Notifiable<Notification>, ICommand
{
    public ChangeActiveVisitantCommand(bool active, Guid id)
    {
        Active = active;
        Id = id;
    }

    public bool Active { get; set; }
    public Guid Id { get; set; }
    public void Validate()
    {
        //don't need validation
    }
}