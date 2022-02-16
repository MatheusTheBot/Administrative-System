using Domain.Entities.Contracts;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Visitant : Entity
{
    public Visitant(Name name, string email, string phoneNumber, Document document, bool active)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Document = document;
        Active = active;
    }

    public Visitant()
    {

    }

    public Name Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public Document Document { get; private set; }
    public bool Active { get; private set; } = false;

    public Apart Apart { get; set; }
    public int ApartId { get; set; }
    public int ApartId2 { get; set; }

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