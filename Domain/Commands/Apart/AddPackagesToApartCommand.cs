using Domain.Commands.Contracts;
using Domain.Commands.Packages;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddPackagesToApartCommand : Notifiable<Notification>, ICommand
{
    public AddPackagesToApartCommand(List<CreatePackageCommand> packageCommands)
    {
        PackageCommands = packageCommands;

        Validate();
    }

    public List<CreatePackageCommand> PackageCommands { get; set; }

    public void AddCommand(CreatePackageCommand command)
    {
        PackageCommands.Add(command);

        Validate();
    }
    public void AddCommand(List<CreatePackageCommand> command)
    {
        foreach (var item in command)
        {
            PackageCommands.Add(item);
        }

        Validate();
    }
    public void Validate()
    {
        foreach (var item in PackageCommands)
            if (!item.IsValid)
                AddNotification(new Notification("PackageCommands", "Invalid Package"));
    }
}