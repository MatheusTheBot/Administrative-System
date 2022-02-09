using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class DeletePackageFromApartCommand : Notifiable<Notification>, ICommand
{
    public DeletePackageFromApartCommand(Guid entityId, Guid itemId)
    {
        EntityId = entityId;
        ItemId = itemId;

        Validate();
    }
    public Guid EntityId { get; set; }
    public Guid ItemId { get; set; }

    public void Validate()
    {
        if (EntityId.ToString() == null)
            AddNotification(new Notification("Id", "Id can't be null"));
        if (ItemId.ToString() == null)
            AddNotification(new Notification("Id", "Id can't be null"));
    }
}