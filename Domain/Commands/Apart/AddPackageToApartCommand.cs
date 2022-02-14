using Domain.Commands.Contracts;
using Domain.Commands.Packages;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddPackageToApartCommand : Notifiable<Notification>, ICommand
{
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
    }
}