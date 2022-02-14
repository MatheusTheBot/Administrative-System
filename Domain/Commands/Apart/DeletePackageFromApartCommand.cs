using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class DeletePackageFromApartCommand : Notifiable<Notification>, ICommand
{
    public DeletePackageFromApartCommand(int apart, int block, Guid itemId)
    {
        Apart = apart;
        Block = block;
        ItemId = itemId;

        Validate();
    }

    public int Apart { get; set; }
    public int Block { get; set; }
    public Guid ItemId { get; set; }

    public void Validate()
    {
        if (Apart == 0)
            AddNotification(new Notification("Apart", "Apart can't be 0"));
        if (Block == 0)
            AddNotification(new Notification("Block", "Block can't be 0"));
        if (ItemId.ToString() == null)
            AddNotification(new Notification("Id", "Id can't be null"));
    }
}