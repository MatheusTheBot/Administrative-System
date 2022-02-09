using Domain.Commands.Contracts;
using Domain.Commands.Visitant;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddVisitantToApartCommand : Notifiable<Notification>, ICommand
{
    public AddVisitantToApartCommand(Guid id, CreateVisitantCommand visitant)
    {
        Id = id;
        Visitant = visitant;

        Validate();
    }

    public Guid Id { get; set; }
    public CreateVisitantCommand Visitant { get; set; }

    public void Validate()
    {
        if (!Visitant.IsValid)
            AddNotification(new Notification("VisitantCommand", "Inválid vistant"));
    }
}