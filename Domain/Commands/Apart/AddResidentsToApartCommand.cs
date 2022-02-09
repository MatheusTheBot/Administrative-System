using Domain.Commands.Contracts;
using Domain.Commands.Resident;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddResidentToApartCommand : Notifiable<Notification>, ICommand
{
    public AddResidentToApartCommand(Guid id, CreateResidentCommand resident)
    {
        Id = id;
        Resident = resident;

        Validate();
    }

    public Guid Id { get; set; }
    public CreateResidentCommand Resident { get; set; }

    public void Validate()
    {
        if (!Resident.IsValid)
            AddNotification(new Notification("ResidentCommand", "Inválid resident"));
    }
}