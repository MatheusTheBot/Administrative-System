using Domain.Commands.Contracts;
using Domain.Commands.Resident;
using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Commands.Apart;
public class AddResidentToApartCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public AddResidentToApartCommand(CreateResidentCommand resident)
    {
        Resident = resident;

        Validate();
    }

    public CreateResidentCommand Resident { get; set; }

    public void Validate()
    {
        if (!Resident.IsValid)
            AddNotification(new Notification("ResidentCommand", "Inválid resident"));
    }
}