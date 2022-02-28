using Domain.Commands.Contracts;
using Flunt.Notifications;

namespace Domain.Commands.Login;
public class LoginCommand : Notifiable<Notification>, ICommand
{
    public LoginCommand(Guid id, string password, string role, int number = 0, int block = 0)
    {
        Id = id;
        Password = password;
        Role = role;
        Number = number;
        Block = block;

        Validate();
    }

    public Guid Id { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int Number { get; set; }
    public int Block { get; set; }

    public void Validate()
    {
        if (Id == Guid.Empty)
            AddNotification(new Notification("Id", "Id can't be empty"));
        //TODO
    }
}