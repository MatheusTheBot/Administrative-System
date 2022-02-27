using Domain.Entities.Contracts;
using Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Entities;
public class Administrator : Entity
{
    public Administrator(Name name, Document document, string email, string phoneNumber, string password)
    {
        Name = name;
        Document = document;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }
    public Administrator()
    {

    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Role { get; private set; } = "Admin";

    [JsonIgnore]
    public string Password { get; private set; }

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