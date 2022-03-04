﻿using Domain.Commands.Contracts;
using Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domain.Commands.Resident;
public class CreateResidentCommand : Notifiable<Notification>, ICommand
{
    [JsonConstructor]
    public CreateResidentCommand(string firstName, string lastName, string email, string phoneNumber, EDocumentType type, string documentNumber, int number, int block, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Type = type;
        DocumentNumber = documentNumber;
        Number = number;
        Block = block;
        Password = password;

        Validate();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public EDocumentType Type { get; set; } = EDocumentType.CPF;
    public string DocumentNumber { get; set; }
    public int Number { get; set; }
    public int Block { get; set; }
    public string Password { get; set; }

    public void Validate()
    {
        PhoneNumber = PhoneNumber.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace(" ", "");
        DocumentNumber = DocumentNumber.Replace(".", "").Replace("/", "").Replace("-", "").Replace(" ", "");

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(FirstName, "FirstName")
            .IsNotNullOrWhiteSpace(LastName, "LastName")
            .IsGreaterOrEqualsThan(FirstName.Length, 3, "FirstName")
            .IsGreaterOrEqualsThan(LastName.Length, 3, "LastName")
            .IsNotNullOrWhiteSpace(Email, "Email")
            .IsEmail(Email, "Email")
            .IsNotNullOrWhiteSpace(PhoneNumber, "PhoneNumber")
            .IsBetween(PhoneNumber.Length, 10, 14, "PhoneNumber")
            .IsNotNullOrWhiteSpace(DocumentNumber, "DocumentNumber")
            .IsBetween(Number, 1, 99999, "Number")
            .IsBetween(Block, 1, 99, "Block")
            .IsNotNullOrWhiteSpace(Password, "Password")
            .IsBetween(Password.Length, 8, 99, "Password")
            );
        if (DocumentNumber.Length != 11 && Type == EDocumentType.CPF)
            AddNotification(DocumentNumber, "Invalid document");
        if (DocumentNumber.Length != 14 && Type == EDocumentType.CNPJ)
            AddNotification(DocumentNumber, "Invalid document");
        foreach (char c in PhoneNumber)
        {
            if (!char.IsDigit(c))
            {
                AddNotification(PhoneNumber, "Invalid number");
            }
        }
        foreach (char c in DocumentNumber)
        {
            if (!char.IsDigit(c))
            {
                AddNotification(DocumentNumber, "Invalid document number");
            }

        }
    }
}