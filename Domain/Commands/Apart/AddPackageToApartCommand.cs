using Domain.Commands.Contracts;
using Domain.Commands.Packages;
using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Commands.Apart;
public class AddPackageToApartCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public AddPackageToApartCommand(CreatePackageCommand package)
    {
        Package = package;

        Validate();
    }

    public CreatePackageCommand Package { get; set; }

    public void Validate()
    {
        if (!Package.IsValid)
            AddNotifications(Package.Notifications);
    }
}