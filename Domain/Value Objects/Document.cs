using Domain.Enums;

namespace Domain.ValueObjects;

public class Document
{
    public Document(EDocumentType type, string number)
    {
        Type = type;
        Number = number;
    }

    public EDocumentType Type { get; private set; } = EDocumentType.CPF;
    public string Number { get; private set; }
}