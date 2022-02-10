using Domain.Commands.Packages;
using Domain.Entities;
using Domain.Handlers.Contracts;
using Domain.Repository;
using Flunt.Notifications;

namespace Domain.Handlers;
public class PackagesHandler : Notifiable<Notification>, 
        IHandler<CreatePackageCommand>,
        IHandler<UpdatePackageCommand>,
        IHandler<ChangePackageTypeCommand>
{
    private readonly IRepository<Packages> Repos;
    public Packages Packages { get; set; }

    public PackagesHandler(IRepository<Packages> repos)
    {
        Repos = repos;
    }

    public IHandlerResult Handle(CreatePackageCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var pack = new Packages(command.BarCode, command.Type, command.Addressee, command.Sender, command.SenderAddress, command.ItemName);

        Repos.Create(pack);

        return new HandlerResult(true, pack);
    }

    public IHandlerResult Handle(UpdatePackageCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var pack = Repos.GetById(command.PackageId);

        pack.UpdatePackage(command.PackageId, command.BarCode, command.ItemName, command.Type, command.Addressee, command.Sender, command.SenderAddress);

        Repos.Update(pack);
        return new HandlerResult(true, pack);
    }

    public IHandlerResult Handle(ChangePackageTypeCommand command)
    {
        if (!command.IsValid)
            return new HandlerResult(false, command.Notifications);

        var pack = Repos.GetById(command.Guid);

        pack.ChangePackageType(command.Guid, command.Type);

        Repos.Update(pack);
        return new HandlerResult(true, pack);
    }
}