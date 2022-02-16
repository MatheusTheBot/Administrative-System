using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Commands.Resident;
public class ChangePhoneNumberResidentCommand : Notifiable<Notification>, ICommand
{
    public ChangePhoneNumberResidentCommand(string phoneNumber, Guid id)
    {
        PhoneNumber = phoneNumber;
        Id = id;

        Validate();
    }

    public string PhoneNumber { get; set; }
    public Guid Id { get; set; }

    public void Validate()
    {
        PhoneNumber = PhoneNumber.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace(" ", "");

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(PhoneNumber, PhoneNumber)
            .IsBetween(PhoneNumber.Length, 10, 13, PhoneNumber)
            .IsFalse(Equals(Id, Guid.Empty), "Id")
            );
        foreach (char c in PhoneNumber)
        {
            if (!char.IsDigit(c))
            {
                AddNotification(PhoneNumber, "Invalid number");
            }
        }
    }
}