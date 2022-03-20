using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domain.Commands.Resident;
public class ChangePhoneNumberResidentCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public ChangePhoneNumberResidentCommand(string phoneNumber, Guid id, int number = 0, int block = 0)
    {
        PhoneNumber = phoneNumber;
        Id = id;
        Number = number;
        Block = block;

        Validate();
    }

    public string PhoneNumber { get; set; }
    public Guid Id { get; set; }
    public int Number { get; set; }
    public int Block { get; set; }

    public void Validate()
    {
        PhoneNumber = PhoneNumber.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace(" ", "");

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(PhoneNumber, "PhoneNumber")
            .IsBetween(PhoneNumber.Length, 10, 14, "PhoneNumber")
            .IsFalse(Equals(Id, Guid.Empty), "Id")
            );
        foreach (char c in PhoneNumber)
        {
            if (!char.IsDigit(c))
            {
                AddNotification(PhoneNumber, "Invalid number");
                continue;
            }
        }
    }
}