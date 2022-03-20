using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domain.Commands.Apart;
public class CreateApartCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public CreateApartCommand(int number, int block)
    {
        Number = number;
        Block = block;

        Validate();
    }

    public int Number { get; set; }
    public int Block { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsBetween(Number, 1, 99999, "Number")
            .IsBetween(Block, 1, 99, "Block")
        );
    }
}