using Domain.Commands.Contracts;
using Domain.Commands.Resident;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddResidentToApartCommand : Notifiable<Notification>, ICommand
{
    public AddResidentToApartCommand(int apart, int block, CreateResidentCommand resident)
    {
        Apart = apart;
        Block = block;
        Resident = resident;

        Validate();
    }

    public int Apart { get; set; }
    public int Block { get; set; }
    public CreateResidentCommand Resident { get; set; }

    public void Validate()
    {
        if (!Resident.IsValid)
            AddNotification(new Notification("ResidentCommand", "Inválid resident"));
        if (Apart == 0)
            AddNotification(new Notification("Apart", "Apart can't be 0"));
        if (Block == 0)
            AddNotification(new Notification("Block", "Block can't be 0"));
    }
}