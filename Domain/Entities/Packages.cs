using Domain.Entities.Contracts;
using Domain.Enums;

namespace Domain.Entities;
public class Packages : Entity
{
    public Packages(string barCode, EPackageType type, string addressee, string sender, string senderAddress, string itemName = "")
    {
        BarCode = barCode;
        ItemName = itemName;
        Type = type;
        Addressee = addressee;
        Sender = sender;
        SenderAddress = senderAddress;
    }

    public string BarCode { get; private set; }
    public string ItemName { get; private set;}
    public EPackageType Type { get; private set; } = EPackageType.SmallPackage;
    public string Addressee { get; private set; }
    public string Sender { get; private set; }
    public string SenderAddress { get; private set; }

    public void UpdatePackage(Guid comparerId, string newBarCode, string newItemName, EPackageType newType,  string newAddressee, string newSender, string newSenderAddress)
    {
        if (Id != comparerId)
            return;

        BarCode = newBarCode;
        ItemName = newItemName;
        Type = newType;
        Addressee = newAddressee;
        Sender = newSender;
        SenderAddress = newSenderAddress;
    }

    public void ChangePackageType(Guid comparerId, EPackageType newType)
    {
        if (Id == comparerId)
            Type = newType;
    }
}