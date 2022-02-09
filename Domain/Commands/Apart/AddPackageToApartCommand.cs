using Domain.Commands.Contracts;
using Domain.Commands.Packages;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddPackageToApartCommand : Notifiable<Notification>, ICommand
{
    public AddPackageToApartCommand(Guid idToAdd, CreatePackageCommand package)
    {
        Id = idToAdd;
        Package = package;

        Validate();
    }

    public Guid Id { get; set; }
    public CreatePackageCommand Package { get; set; }

    public void Validate()
    {
        if (!Package.IsValid)
            AddNotification(new Notification("PackageCommand", "Invalid Package"));
    }
}