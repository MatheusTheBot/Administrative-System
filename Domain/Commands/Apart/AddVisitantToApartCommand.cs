using Domain.Commands.Contracts;
using Domain.Commands.Visitant;
using Flunt.Notifications;

namespace Domain.Commands.Apart;
public class AddVisitantToApartCommand : Notifiable<Notification>, ICommand
{
    public AddVisitantToApartCommand(CreateVisitantCommand command)
    {
        VisitantCommands.Add(command);

        Validate();
    }

    public List<CreateVisitantCommand> VisitantCommands { get; set; } = new();

    public void AddCommand(CreateVisitantCommand command)
    {
        VisitantCommands.Add(command);

        Validate();
    }
    public void AddCommand(List<CreateVisitantCommand> command)
    {
        foreach (var item in command)
        {
            VisitantCommands.Add(item);
        }

        Validate();
    }
    public void Validate()
    {
        foreach (var item in VisitantCommands)
            if (!item.IsValid)
                AddNotification(new Notification("VisitantCommands", "Inválid visitant"));
    }
}