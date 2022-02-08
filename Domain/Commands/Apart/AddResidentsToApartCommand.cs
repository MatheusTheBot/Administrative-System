using Domain.Commands.Contracts;
using Domain.Commands.Resident;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddResidentsToApartCommand : Notifiable<Notification>, ICommand
{
    public AddResidentsToApartCommand(CreateResidentCommand residentCommand)
    {
        ResidentList.Add(residentCommand);

        Validate();
    }

    public List<CreateResidentCommand> ResidentList { get; set; } = new();

    public void AddCommand(CreateResidentCommand command)
    {
        ResidentList.Add(command);

        Validate();
    }
    public void AddCommand(List<CreateResidentCommand> command)
    {
        foreach(var item in command)
        {
            ResidentList.Add(item);
        }

        Validate();
    }
    public void Validate()
    {
        foreach(var res in ResidentList)
            if (!res.IsValid)
                AddNotification(new Notification("ResidentCommand", "Inválid resident"));
    }
}