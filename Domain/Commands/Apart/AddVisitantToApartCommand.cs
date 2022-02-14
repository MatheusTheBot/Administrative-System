using Domain.Commands.Contracts;
using Domain.Commands.Visitant;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddVisitantToApartCommand : Notifiable<Notification>, ICommand
{
    public AddVisitantToApartCommand(int apart, int block, CreateVisitantCommand visitant)
    {
        Apart = apart;
        Block = block;
        Visitant = visitant;

        Validate();
    }

    public int Apart { get; set; }
    public int Block { get; set; }
    public CreateVisitantCommand Visitant { get; set; }

    public void Validate()
    {
        if (!Visitant.IsValid)
            AddNotification(new Notification("VisitantCommand", "Inválid vistant"));
        if (Apart == 0)
            AddNotification(new Notification("Apart", "Apart can't be 0"));
        if (Block == 0)
            AddNotification(new Notification("Block", "Block can't be 0"));
    }
}