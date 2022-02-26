using Domain.Commands.Contracts;
using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Commands.Visitant;
public class ChangeActiveVisitantCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public ChangeActiveVisitantCommand(bool active, Guid id)
    {
        Active = active;
        Id = id;

        Validate();
    }

    public bool Active { get; set; }
    public Guid Id { get; set; }
    public void Validate()
    {
        if (Id == Guid.Empty)
            AddNotification("Id", "Id can't be empty");
    }
}