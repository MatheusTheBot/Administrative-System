﻿using Domain.Entities.Contracts;
using Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Entities;
public class Resident : Entity
{
    public Resident(Name name, string email, string phoneNumber, Document document, int number, int block)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Document = document;
        Number = number;
        Block = block;
    }

    public Resident()
    {

    }

    public Name Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public Document Document { get; private set; }

    [JsonIgnore]
    public Apart Apart { get; set; }
    public int Number { get; set; }
    public int Block { get; set; }

    public void ChangeName(Name newName)
    {
        Name = newName;
    }

    public void ChangeEmail(string email)
    {
        Email = email;
    }

    public void ChangePhoneNumber(string newNumber)
    {
        PhoneNumber = newNumber;
    }

    public void ChangeDocument(Document newDocument)
    {
        Document = newDocument;
    }
}
