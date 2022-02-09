using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class DeleteVisitantFromApartCommand : Notifiable<Notification>, ICommand
{
    public DeleteVisitantFromApartCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }

    public void Validate()
    {
        if (Id.ToString() == null)
            AddNotification(new Notification("Id", "Id can't be null"));
    }
}