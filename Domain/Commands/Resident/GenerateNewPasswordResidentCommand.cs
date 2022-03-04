using Domain.Commands.Contracts;
using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Commands.Resident;
public class GenerateNewPasswordResidentCommand : Notifiable<Notification>, ICommand
{
    public GenerateNewPasswordResidentCommand(string password, Guid id)
    {
        Password = password;
        Id = id;
    }

    [JsonIgnore]
    public string NewPassword { get; set; }
    public string Password { get; set; }
    public Guid Id { get; set; }

    public void Validate()
    {
        //TODO
    }
}