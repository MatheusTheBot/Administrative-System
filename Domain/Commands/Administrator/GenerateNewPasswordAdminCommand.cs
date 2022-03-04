using Domain.Commands.Contracts;
using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Commands.Administrator;
public class GenerateNewPasswordAdminCommand : Notifiable<Notification>, ICommand
{
    public GenerateNewPasswordAdminCommand(string password, Guid id)
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