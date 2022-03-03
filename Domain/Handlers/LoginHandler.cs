using Domain.Commands.Login;
using Domain.Entities;
using Domain.Handlers.Contracts;
using Domain.Repository;
using Domain.Services;
using Flunt.Notifications;

namespace Domain.Handlers;
public class LoginHandler : Notifiable<Notification>, IHandler<LoginCommand>
{
    private readonly IRepository<Administrator> Repository;
    private readonly IRepository<Resident> ResidentRepository;
    public LoginHandler(IRepository<Administrator> repository, IRepository<Resident> residentRepository)
    {
        Repository = repository;
        ResidentRepository = residentRepository;
    }

    public IHandlerResult Handle(LoginCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        if (command.Role == "Admin")
        {
            Administrator? search;
            try
            {
                search = Repository.GetById(command.Id);
            }
            catch (Exception)
            {
                return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
            }
            if (search == null)
                return new HandlerResult(false, "Administrator not found");

            var IsTrue = ServicePassword.Verify(search.Password, command.Password);
            if (IsTrue)
                return new HandlerResult(true, "Access Granted");
            return new HandlerResult(false, "Access Denied");
        }
        if (command.Role == "Resident")
        {
            Resident? search;
            try
            {
                search = ResidentRepository.GetById(command.Number, command.Block, command.Id);
            }
            catch (Exception)
            {
                return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
            }
            if (search == null)
                return new HandlerResult(false, "Resident not found");

            var IsTrue = ServicePassword.Verify(search.Password, command.Password);
            if (IsTrue)
                return new HandlerResult(true, "Access Granted");
            return new HandlerResult(false, "Access Denied");
        }
        return new HandlerResult(false, "Wrong Role, try again");
    }
}