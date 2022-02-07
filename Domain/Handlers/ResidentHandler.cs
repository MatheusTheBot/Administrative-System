using Domain.Commands;
using Domain.Entities;
using Domain.Handlers.Contracts;
using Domain.Repository;
using Domain.ValueObjects;
using Flunt.Notifications;

namespace Domain.Handlers;
public class ResidentHandler :Notifiable<Notification>,
        IHandler<CreateResidentCommand>,
        IHandler<ChangeNameResidentCommand>,
        IHandler<ChangeEmailResidentCommand>,
        IHandler<ChangePhoneNumberResidentCommand>,
        IHandler<ChangeDocumentResidentCommand>
{
    private readonly IRepository<Resident> repos;
    public ResidentHandler(IRepository<Resident> repos)
    {
        this.repos = repos;
    }

    public IHandlerResult Handle(CreateResidentCommand command)
    {
        //fail fast validation
        if (!command.IsValid)
            return new HandlerResult(true, command.Notifications);

        Resident newResident = new Resident(new Name(command.FirstName, command.LastName), command.Email, command.PhoneNumber, new Document(command.Type, command.DocumentNumber));

        try
        {
            repos.Create(newResident);
        }
        catch
        {
            return new HandlerResult(false, "Internal Error");
        }
        return new HandlerResult(true, newResident);
    }

    public IHandlerResult Handle(ChangeNameResidentCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(true, command.Notifications);

        //query

        try
        {
            repos.Create(newResident);
        }
        catch
        {
            return new HandlerResult(false, "Internal Error");
        }
        return new HandlerResult(true, newResident);
    }

    public IHandlerResult Handle(ChangeEmailResidentCommand command)
    {
        throw new NotImplementedException();
    }

    public IHandlerResult Handle(ChangePhoneNumberResidentCommand command)
    {
        throw new NotImplementedException();
    }

    public IHandlerResult Handle(ChangeDocumentResidentCommand command)
    {
        throw new NotImplementedException();
    }
}