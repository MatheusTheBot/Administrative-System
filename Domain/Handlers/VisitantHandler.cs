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
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var visitant = new Visitant(new Name(command.FirstName, command.LastName), command.Email, command.PhoneNumber, new Document(command.Type, command.DocumentNumber), command.Active, command.Number, command.Block);

        try
        {
            Repos.Create(visitant);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, visitant);
    }

    public IHandlerResult Handle(ChangeNameVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(true, command.Notifications);

        //rehydration
        Visitant? visitant;
        try
        {
            visitant = Repos.GetById(command.Id);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        };
        if (visitant == null)
            return new HandlerResult(false, "Visitant not found");

        visitant.ChangeName(new Name(command.FirstName, command.LastName));

        try
        {
            Repos.Update(visitant);
        }
        catch
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, visitant);
    }

    public IHandlerResult Handle(ChangeEmailVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(true, command.Notifications);

        Visitant? visitant;
        try
        {
            visitant = Repos.GetById(command.Id);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        };
        if (visitant == null)
            return new HandlerResult(false, "Visitant not found");

        visitant.ChangeEmail(command.Email);

        try
        {
            Repos.Update(visitant);
        }
        catch
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, visitant);
    }

    public IHandlerResult Handle(ChangePhoneNumberVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(true, command.Notifications);

        Visitant? visitant;
        try
        {
            visitant = Repos.GetById(command.Id);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        };
        if (visitant == null)
            return new HandlerResult(false, "Visitant not found");

        visitant.ChangePhoneNumber(command.PhoneNumber);

        try
        {
            Repos.Update(visitant);
        }
        catch
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, visitant);
    }

    public IHandlerResult Handle(ChangeDocumentVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(true, command.Notifications);

        //rehydration
        Visitant? visitant;
        try
        {
            visitant = Repos.GetById(command.Id);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        };
        if (visitant == null)
            return new HandlerResult(false, "Visitant not found");

        visitant.ChangeDocument(new Document(command.Type, command.DocumentNumber));

        try
        {
            Repos.Update(visitant);
        }
        catch
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, visitant);
    }

    public IHandlerResult Handle(ChangeActiveVisitantCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(true, command.Notifications);

        //rehydration
        Visitant? visitant;
        try
        {
            visitant = Repos.GetById(command.Id);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        };
        if (visitant == null)
            return new HandlerResult(false, "Visitant not found");

        if (command.Active == true)
            visitant.IsActive();
        if (command.Active == false)
            visitant.IsInactive();

        try
        {
            Repos.Update(visitant);
        }
        catch
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, visitant);
    }
}