using Domain.Commands.Apart;
using Domain.Entities;
using Domain.Handlers.Contracts;
using Domain.Repository;
using Domain.ValueObjects;
using Flunt.Notifications;

namespace Domain.Handlers;
public class ApartHandler : Notifiable<Notification>,
        IHandler<CreateApartCommand>,
        IHandler<AddPackageToApartCommand>,
        IHandler<AddResidentToApartCommand>,
        IHandler<AddVisitantToApartCommand>,
        IHandler<DeletePackageFromApartCommand>,
        IHandler<DeleteResidentFromApartCommand>,
        IHandler<DeleteVisitantFromApartCommand>
{
    private readonly IRepository<Apart> Repos;
    public ApartHandler(IRepository<Apart> repos)
    {
        Repos = repos;
    }

    public IHandlerResult Handle(CreateApartCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        //verify if already exists
        Apart? search;

        try
        {
            search = Repos.GetById(command.Number, command.Block);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        if (search != null)
            return new HandlerResult(false, "Already have a Apart with that number and block");

        var apart = new Apart(command.Number, command.Block);

        try
        {
            Repos.Create(apart);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Internal Error, unable to add a new apartment");
        }
        return new HandlerResult(true, apart);
    }

    public IHandlerResult Handle(AddPackageToApartCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        Apart? search;

        try
        {
            search = Repos.GetById(command.Apart, command.Block);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }

        if (search == null)
            return new HandlerResult(false, "Apart not found");

        var pack = new Packages(command.Package.BarCode, command.Package.Type, command.Package.Addressee, command.Package.Sender, command.Package.SenderAddress, command.Package.Number, command.Package.Block, command.Package.ItemName);

        search.AddPackage(pack);

        return new HandlerResult(true, search);
    }

    public IHandlerResult Handle(AddResidentToApartCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        Apart? search;

        try
        {
            search = Repos.GetById(command.Apart, command.Block);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        if (search == null)
            return new HandlerResult(false, "Apart not found");

        var resident = new Resident(new Name(command.Resident.FirstName, command.Resident.LastName), command.Resident.Email, command.Resident.PhoneNumber, new Document(command.Resident.Type, command.Resident.DocumentNumber), command.Resident.Number, command.Resident.Block);

        search.AddResident(resident);

        try
        {
            Repos.Update(search);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }

        return new HandlerResult(true, search);
    }

    public IHandlerResult Handle(AddVisitantToApartCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        Apart? search;

        try
        {
            search = Repos.GetById(command.Apart, command.Block);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        if (search == null)
            return new HandlerResult(false, "Apart not found");

        var visitant = new Visitant(new Name(command.Visitant.FirstName, command.Visitant.LastName), command.Visitant.Email, command.Visitant.PhoneNumber, new Document(command.Visitant.Type, command.Visitant.DocumentNumber), command.Visitant.Active, command.Visitant.Number, command.Visitant.Block);

        search.AddVisitant(visitant);

        try
        {
            Repos.Update(search);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }

        return new HandlerResult(true, search);
    }

    public IHandlerResult Handle(DeletePackageFromApartCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        Apart? search;

        try
        {
            search = Repos.GetById(command.Apart, command.Block);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        if (search == null)
            return new HandlerResult(false, "Apart not found");

        search.RemovePackage(command.ItemId);

        try
        {
            Repos.Update(search);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }

        return new HandlerResult(true, search);
    }

    public IHandlerResult Handle(DeleteResidentFromApartCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        Apart? search;

        try
        {
            search = Repos.GetById(command.Apart, command.Block);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        if (search == null)
            return new HandlerResult(false, "Apart not found");

        search.RemoveResident(command.ItemId);

        try
        {
            Repos.Update(search);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }

        return new HandlerResult(true, search);
    }

    public IHandlerResult Handle(DeleteVisitantFromApartCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);


        Apart? search;

        try
        {
            search = Repos.GetById(command.Apart, command.Block);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        if (search == null)
            return new HandlerResult(false, "Apart not found");

        search.RemoveVisitant(command.ItemId);

        try
        {
            Repos.Update(search);
        }
        catch (Exception)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }

        return new HandlerResult(true, search);
    }
}