using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Apart
{
    public class DeleteResidentFromApartCommand : Notifiable<Notification>, ICommand
    {
        public DeleteResidentFromApartCommand(Guid id)
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