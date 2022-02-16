using Domain.Commands.Visitant;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.CommandsTests;

[TestClass]
public class VisitantCommandsTest
{
    private readonly string FirstName = "Matheus";
    private readonly string LastName = "Ferreira";
    private readonly string Email = "matheus.henrique123@gmail.com";
    private readonly string PhoneNumber = "+55 (41) 9 1234-1234";
    private readonly EDocumentType DocumentTypeCPF = EDocumentType.CPF;
    private readonly EDocumentType DocumentTypeCNPJ = EDocumentType.CNPJ;
    private readonly string DocumentNumberCPF = "123.456.789-12";
    private readonly string DocumentNumberCNPJ = "67.573.684/0001-91";
    private readonly bool Active = true;

    private readonly Guid Id = Guid.Parse("0ecdb2d6-0021-4c0f-8e4f-14080940715b");

    public VisitantCommandsTest()
    {
        //correctCommand = new CreateVisitantCommand(FirstName, LastName, Email, PhoneNumber, DocumentType, DocumentNumber);
    }

    //Red, Green, Refactor

    [TestMethod]
    public void DadoUmFirstNamePequenoCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new CreateVisitantCommand(_name, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmFirstNameVazioCommandDeveRetornarErro()
    {
        var command = new CreateVisitantCommand("    ", LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNamePequenoCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new CreateVisitantCommand(FirstName, _name, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNameVazioCommandDeveRetornarErro()
    {
        var command = new CreateVisitantCommand(FirstName, "  ", Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmEmailVazioCommandDeveRetornarErro()
    {
        var command = new CreateVisitantCommand(FirstName, LastName, " ", PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmEmailErradoCommandDeveRetornarErro()
    {
        var _email = "çoerfp9ótp5o´43ot[";
        // "lhkliakgfjh";
        // "lhkliakgf.h;hd";
        // "çoerfp9ótp5o´43ot[";

        var command = new CreateVisitantCommand(FirstName, LastName, _email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmPhoneNumberPequenoCommandDeveRetornarErro()
    {
        var _phone = "12 1234-123";

        var command = new CreateVisitantCommand(FirstName, LastName, Email, _phone, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmPhoneNumberGrandeCommandDeveRetornarErro()
    {
        var _phone = "+55 12 91234-12345";

        var command = new CreateVisitantCommand(FirstName, LastName, Email, _phone, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmPhoneNumberErradoCommandDeveRetornarErro()
    {
        var _phone = "12 ;1234-1234";
        //"12 a1234-1234"
        //"12 /1234-1234"
        //"12 *1234-1234"

        var command = new CreateVisitantCommand(FirstName, LastName, Email, _phone, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmDocumentNumberPequenoCommandDeveRetornarErro()
    {
        var _doc = "123.456.789-1";

        var command = new CreateVisitantCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, _doc, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmDocumentNumberGrandeCommandDeveRetornarErro()
    {
        var _doc = "123.456.789-123";

        var command = new CreateVisitantCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, _doc, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmDocumentNumberCorretoEUmDocumentTypeInválidoCommandDeveRetornarErro()
    {
        var command = new CreateVisitantCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCNPJ, DocumentNumberCPF, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmDocumentNumber_2_CorretoEUmDocumentTypeInválidoCommandDeveRetornarErro()
    {
        var command = new CreateVisitantCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCNPJ, Active);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmCommandVálidoCommandDeveRetornarTrue()
    {
        var command = new CreateVisitantCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
        Assert.AreEqual(true, command.IsValid);
    }



    //Change name

    [TestMethod]
    public void DadoUmFirstNamePequenoChangeNameCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new ChangeNameVisitantCommand(_name, LastName, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmFirstNameVazioChangeNameCommandDeveRetornarErro()
    {
        var command = new ChangeNameVisitantCommand("  ", LastName, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNamePequenoChangeNameCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new ChangeNameVisitantCommand(FirstName, _name, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNameVazioChangeNameCommandDeveRetornarErro()
    {
        var command = new ChangeNameVisitantCommand(FirstName, "   ", Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmIdInválidoChangeNameCommandDeveRetornarErro()
    {
        var command = new ChangeNameVisitantCommand(FirstName, LastName, Guid.Empty);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoCommandVálidoChangeNameCommandDeveRetornarTrue()
    {
        var command = new ChangeNameVisitantCommand(FirstName, LastName, Guid.NewGuid());
        Assert.AreEqual(true, command.IsValid);
    }



    //ChangeDocs

    [TestMethod]
    public void DadoTypeVálidoEUmDocumentNumberInválidoChangeDocumentCommandDeveRetornarErro()
    {
        var command = new ChangeDocumentVisitantCommand(DocumentTypeCPF, DocumentNumberCNPJ, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoTypeInválidoEUmDocumentNumberVálidoChangeDocumentCommandDeveRetornarErro2()
    {
        var command = new ChangeDocumentVisitantCommand(DocumentTypeCNPJ, DocumentNumberCPF, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmIdInválidoChangeDocumentCommandDeveRetornarErro()
    {
        var command = new ChangeDocumentVisitantCommand(DocumentTypeCNPJ, DocumentNumberCPF, Guid.Empty);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoCommandVálidoChangeDocumentCommandDeveRetornarTrue()
    {
        var command = new ChangeDocumentVisitantCommand(DocumentTypeCPF, DocumentNumberCPF, Id);
        Assert.AreEqual(true, command.IsValid);
    }



    //ChangeEmail

    [TestMethod]
    public void DadoEmailInválidoChangeEmailCommandDeveRetornarErro()
    {
        //https://github.com/andrebaltieri/Flunt/blob/main/Flunt.Tests/EmailValidationTests.cs
        var _email = "aaaa@aaaaa";
        var command = new ChangeEmailVisitantCommand(_email, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoIdInválidoChangeEmailCommandDeveRetornarErro()
    {
        var command = new ChangeEmailVisitantCommand(Email, Guid.Empty);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoCommandVálidoChangeEmailCommandDeveRetornarTrue()
    {
        var command = new ChangeEmailVisitantCommand(Email, Id);
        Assert.AreEqual(true, command.IsValid);
    }



    //ChangePhone

    [TestMethod]
    public void DadoNumeroInválidoChangePhoneCommandDeveRetornarErro()
    {
        var _number = "4568a21347";
        var command = new ChangePhoneNumberVisitantCommand(_number, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoIdInválidoChangePhoneCommandDeveRetornarErro()
    {
        var command = new ChangePhoneNumberVisitantCommand(PhoneNumber, Guid.Empty);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoCommandVálidoChangePhoneCommandDeveRetornarTrue()
    {
        var command = new ChangePhoneNumberVisitantCommand(PhoneNumber, Id);
        Assert.AreEqual(true, command.IsValid);
    }
}