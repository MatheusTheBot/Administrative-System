﻿using Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Commands;
public class ChangePhoneNumberCommand : Notifiable<Notification>, ICommand
{
    public ChangePhoneNumberCommand(string phoneNumber, Guid id)
    {
        PhoneNumber = phoneNumber;
        Id = id;

        Validate();
    }

    public string PhoneNumber { get;  set; }
    public Guid Id { get; set; }

    public void Validate()
    {
        PhoneNumber = PhoneNumber.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace(" ", "");

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(PhoneNumber, PhoneNumber)
            .IsBetween(PhoneNumber.Length, 10, 13, PhoneNumber)
            );
        foreach(char c in PhoneNumber)
        {
            if (!char.IsDigit(c))
            {
                AddNotification(PhoneNumber, "Invalid number");
            }
        }
    }
}