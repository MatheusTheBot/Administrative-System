using Domain.Commands.Visitant;
using Domain.Entities;
using Domain.Handlers.Contracts;
using Domain.Repository;
using Domain.ValueObjects;
using Flunt.Notifications;

namespace Domain.Handlers;
public class VisitantHandler : Notifiable<Notification>, 
        IHandler<CreateVisitantCommand>,
        IHandler<ChangeNameVisitantCommand>,
        IHandler<ChangeEmailVisitantCommand>,
        IHandler<ChangePhoneNumberVisitantCommand>,
        IHandler<ChangeDocumentVisitantCommand>,
        IHandler<ChangeActiveVisitantCommand>
{
    public VisitantHandler(IRepository<Visitant> repos)
    {
        Repos = repos;
    }

    public IRepository<Visitant> Repos { get; set; }


    public IHandlerResult Handle(CreateVisitantCommand command)
    {
        if(!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var vis = new Visitant(new Name(command.FirstName, command.LastName), command.Email, command.PhoneNumber, new Document(command.Type, command.DocumentNumber), command.Active);

        Repos.Create(vis);
        return new HandlerResult(true, vis);
    }

    public IHandlerResult Handle(ChangeNameVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var vis = Repos.GetById(command.Id);
        vis.ChangeName(new Name(command.FirstName, command.LastName));

        Repos.Update(vis);
        return new HandlerResult(true, vis);
    }

    public IHandlerResult Handle(ChangeEmailVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var vis = Repos.GetById(command.Id);
        vis.ChangeEmail(command.Email);

        Repos.Update(vis);
        return new HandlerResult(true, vis);
    }

    public IHandlerResult Handle(ChangePhoneNumberVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var vis = Repos.GetById(command.Id);
        vis.ChangePhoneNumber(command.PhoneNumber);

        Repos.Update(vis);
        return new HandlerResult(true, vis);
    }

    public IHandlerResult Handle(ChangeDocumentVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var vis = Repos.GetById(command.Id);
        vis.ChangeDocument(new Document(command.Type, command.DocumentNumber));

        Repos.Update(vis);
        return new HandlerResult(true, vis);
    }

    public IHandlerResult Handle(ChangeActiveVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var vis = Repos.GetById(command.Id);

        if (command.Active == true)
            vis.IsActive();
        if(command.Active == false)
            vis.IsInactive();

        Repos.Update(vis);
        return new HandlerResult(true, vis);
    }
}