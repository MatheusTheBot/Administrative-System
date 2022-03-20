using Domain.Commands.Contracts;
using Domain.Commands.Visitant;
using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Commands.Apart;
public class AddVisitantToApartCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public AddVisitantToApartCommand(CreateVisitantCommand visitant)
    {
        Visitant = visitant;

        Validate();
    }

    public CreateVisitantCommand Visitant { get; set; }

    public void Validate()
    {
        if (!Visitant.IsValid)
            AddNotifications(Visitant.Notifications);
    }
}