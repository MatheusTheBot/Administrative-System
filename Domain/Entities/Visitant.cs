using Domain.Entities.Contracts;
using Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Entities;
public class Visitant : Entity
{
    public Visitant(Name name, string email, string phoneNumber, Document document, bool active, int number, int block)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Document = document;
        Active = active;
        Number = number;
        Block = block;
    }

    public Visitant()
    {

    }

    public Name Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public Document Document { get; private set; }
    public bool Active { get; private set; } = false;

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

    public void IsActive() { Active = true; }

    public void IsInactive() { Active = false; }
}