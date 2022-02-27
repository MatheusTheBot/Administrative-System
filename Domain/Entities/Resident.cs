using Domain.Entities.Contracts;
using Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Entities;
public class Resident : Entity
{
    public Resident(Name name, string email, string phoneNumber, Document document, int number, int block, string password)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Document = document;
        Number = number;
        Block = block;
        Password = password;
    }

    public Resident()
    {

    }

    public Name Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public Document Document { get; private set; }

    [JsonIgnore]
    public string Password { get; private set; }
    public string Role { get; private set; } = "Resident";

    [JsonIgnore]
    public Apart Apart { get; private set; }
    public int Number { get; private set; }
    public int Block { get; private set; }

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

    public void ChangePassword(string newPassword)
    {
        Password = newPassword;
    }
}
