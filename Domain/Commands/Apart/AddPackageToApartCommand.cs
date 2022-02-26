using Domain.Commands.Contracts;
using Domain.Commands.Packages;
using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Commands.Apart;
public class AddPackageToApartCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public AddPackageToApartCommand(int apart, int block, CreatePackageCommand package)
    {
        Apart = apart;
        Block = block;
        Package = package;

        Validate();
    }

    public int Apart { get; set; }
    public int Block { get; set; }
    public CreatePackageCommand Package { get; set; }

    public void Validate()
    {
        if (!Package.IsValid)
            AddNotification(new Notification("PackageCommand", "Invalid Package"));
        if (Apart == 0)
            AddNotification(new Notification("Apart", "Apart can't be 0"));
        if (Block == 0)
            AddNotification(new Notification("Block", "Block can't be 0"));
    }
}