﻿using Domain.Commands.Contracts;
using Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using Domain.Entities;

namespace Domain.Commands.Packages;
public class CreatePackageCommand : Notifiable<Notification>, ICommand
{
    public CreatePackageCommand(string barCode, EPackageType type, string addressee, string sender, string senderAddress, string itemName = "")
    {
        BarCode = barCode;
        ItemName = itemName;
        Type = type;
        Addressee = addressee;
        Sender = sender;
        SenderAddress = senderAddress;

        Validate();
    }

    public string BarCode { get;  set; }
    public string ItemName { get;  set; }
    public EPackageType Type { get;  set; } = EPackageType.SmallPackage;
    public string Addressee { get;  set; }
    public string Sender { get;  set; }
    public string SenderAddress { get;  set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(BarCode, BarCode)
            .AreEquals(BarCode.Length, 13, BarCode)
            .IsNotNull(ItemName, ItemName)
            .IsLowerOrEqualsThan(ItemName.Length, 150, ItemName)
            .IsNotNullOrWhiteSpace(Addressee, Addressee)
            .IsLowerOrEqualsThan(Addressee.Length, 250, Addressee) 
            .IsNotNullOrWhiteSpace(Sender, Sender)
            .IsLowerOrEqualsThan(Sender, 150, Sender)
            .IsNotNullOrWhiteSpace(SenderAddress, SenderAddress)
            .IsLowerOrEqualsThan(SenderAddress, 250, SenderAddress)
            );
    }
}