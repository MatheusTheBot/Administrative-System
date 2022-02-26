using Domain.Commands.Contracts;
using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Commands.Apart;
public class DeletePackageFromApartCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public DeletePackageFromApartCommand(int number, int block, Guid itemId)
    {
        Number = number;
        Block = block;
        ItemId = itemId;

        Validate();
    }

    public int Number { get; set; }
    public int Block { get; set; }
    public Guid ItemId { get; set; }

    public void Validate()
    {
        if (Number == 0)
            AddNotification(new Notification("Apart", "Apart can't be 0"));
        if (Block == 0)
            AddNotification(new Notification("Block", "Block can't be 0"));
        if (ItemId == Guid.Empty)
            AddNotification(new Notification("Id", "Id can't be empty"));
    }
}