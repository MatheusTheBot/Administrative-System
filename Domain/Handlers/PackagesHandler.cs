using Domain.Commands.Packages;
using Domain.Entities;
using Domain.Handlers.Contracts;
using Domain.Repository;
using Flunt.Notifications;
using System.Data.Common;

namespace Domain.Handlers;
public class PackagesHandler : Notifiable<Notification>, 
        IHandler<CreatePackageCommand>,
        IHandler<UpdatePackageCommand>,
        IHandler<ChangePackageTypeCommand>
{
    private readonly IRepository<Packages> Repos;
    public PackagesHandler(IRepository<Packages> repos)
    {
        Repos = repos;
    }

    public IHandlerResult Handle(CreatePackageCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var pack = new Packages(command.BarCode, command.Type, command.Addressee, command.Sender, command.SenderAddress, command.ItemName);

        try
        {
            Repos.Create(pack);
        }
        catch (DbException)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, pack);
    }

    public IHandlerResult Handle(UpdatePackageCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        Packages? pack;

        try
        {
            pack = Repos.GetById(command.PackageId);
        }
        catch (DbException)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }

        pack.UpdatePackage(command.PackageId, command.BarCode, command.ItemName, command.Type, command.Addressee, command.Sender, command.SenderAddress);

        try
        {
            Repos.Update(pack);
        }
        catch (DbException)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, pack);
    }

    public IHandlerResult Handle(ChangePackageTypeCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        Packages? pack;

        try
        {
            pack = Repos.GetById(command.Guid);
        }
        catch (DbException)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }

        pack.ChangePackageType(command.Guid, command.Type);

        try
        {
            Repos.Update(pack);
        }
        catch (DbException)
        {
            return new HandlerResult(false, "Unable to access database, unable to perform requested operation");
        }
        return new HandlerResult(true, pack);
    }
}