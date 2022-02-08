using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Apart
{
    public class DeleteResidentFromApartcCommand : Notifiable<Notification>, ICommand
    {
        public DeleteResidentFromApartcCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }

        public void Validate()
        {
            if (Id.ToString() == null)
                AddNotification(new Notification("Id", "Id can't be null"));
        }
    }
}