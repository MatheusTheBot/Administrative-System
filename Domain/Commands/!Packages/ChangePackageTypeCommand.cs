using Domain.Commands.Contracts;
using Domain.Enums;
using Flunt.Notifications;

namespace Domain.Commands.Packages;
public class ChangePackageTypeCommand : Notifiable<Notification>, ICommand
{
    public ChangePackageTypeCommand(Guid guid, EPackageType type)
    {
        Guid = guid;
        Type = type;

        Validate();
    }

    public Guid Guid { get; set; }
    public EPackageType Type { get; set; }
    public void Validate()
    {
        //maybe this don't need to have validations
    }
}