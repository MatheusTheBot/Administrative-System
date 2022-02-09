﻿using Domain.Commands.Contracts;
using Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Commands.Resident;
public class ChangeDocumentResidentCommand : Notifiable<Notification>, ICommand
{
    public ChangeDocumentResidentCommand(EDocumentType type, string documentNumber, Guid id)
    {
        Type = type;
        DocumentNumber = documentNumber;
        Id = id;

        Validate();
    }

    public EDocumentType Type { get; set; } = EDocumentType.CPF;
    public string DocumentNumber { get; set; }
    public Guid Id { get; set; }

    public void Validate()
    {
        DocumentNumber = DocumentNumber.Replace(".", "").Replace("/", "").Replace("-", "").Replace(" ", "");

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(DocumentNumber, DocumentNumber)
            );

        if (DocumentNumber.Length != 11 && Type == EDocumentType.CPF)
            AddNotification(DocumentNumber, "Invalid document");
        if (DocumentNumber.Length != 14 && Type == EDocumentType.CNPJ)
            AddNotification(DocumentNumber, "Invalid document");
    }
}