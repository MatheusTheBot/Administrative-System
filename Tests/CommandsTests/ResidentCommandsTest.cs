using Domain.Commands.Resident;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.CommandsTests;

[TestClass]
public class ResidentCommandsTest
{
    private readonly string FirstName = "Matheus";
    private readonly string LastName = "Ferreira";
    private readonly string Email = "matheus.henrique123@gmail.com";
    private readonly string PhoneNumber = "+55 (041) 9 1234-1234";
    private readonly EDocumentType DocumentTypeCPF = EDocumentType.CPF;
    private readonly EDocumentType DocumentTypeCNPJ = EDocumentType.CNPJ;
    private readonly string DocumentNumberCPF = "123.456.789-12";
    private readonly string DocumentNumberCNPJ = "67.573.684/0001-91";
    private readonly int Apart = 202;
    private readonly int Block = 4;
    private readonly string Password = "fjerwrelifgoi53rh";

    private readonly Guid Id = Guid.Parse("0ecdb2d6-0021-4c0f-8e4f-14080940715b");

    public ResidentCommandsTest()
    {

    }

    //Red, Green, Refactor

    [TestMethod]
    public void DadoUmFirstNamePequenoCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new CreateResidentCommand(_name, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmFirstNameVazioCommandDeveRetornarErro()
    {
        var command = new CreateResidentCommand("    ", LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmFirstNameMinimoCommandDeveRetornarTrue()
    {
        var command = new CreateResidentCommand("Lou", LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(true, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNamePequenoCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new CreateResidentCommand(FirstName, _name, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNameVazioCommandDeveRetornarErro()
    {
        var command = new CreateResidentCommand(FirstName, "  ", Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNameMinimoCommandDeveRetornarTrue()
    {
        var command = new CreateResidentCommand(FirstName, "Jnr", Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(true, command.IsValid);
    }
    [TestMethod]
    public void DadoUmEmailVazioCommandDeveRetornarErro()
    {
        var command = new CreateResidentCommand(FirstName, LastName, " ", PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmEmailInválidoCommandDeveRetornarErro()
    {
        var _email = "çoerfp9ótp5o´43ot[";
        // "lhkliakgfjh";
        // "lhkliakgf.h;hd";
        // "çoerfp9ótp5o´43ot[";

        var command = new CreateResidentCommand(FirstName, LastName, _email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmPhoneNumberPequenoCommandDeveRetornarErro()
    {
        var _phone = "12 1234-123";

        var command = new CreateResidentCommand(FirstName, LastName, Email, _phone, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmPhoneNumberGrandeCommandDeveRetornarErro()
    {
        var _phone = "+55 012 9 1234-1234 5";

        var command = new CreateResidentCommand(FirstName, LastName, Email, _phone, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmPhoneNumberInválidoCommandDeveRetornarErro()
    {
        var _phone = "12 ;1234-1234";
        //"12 a1234-1234"
        //"12 /1234-1234"
        //"12 *1234-1234"

        var command = new CreateResidentCommand(FirstName, LastName, Email, _phone, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmPhoneNumberMinimoCommandDeveRetornarTrue()
    {
        var _phone = "12 1234-1234";

        var command = new CreateResidentCommand(FirstName, LastName, Email, _phone, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(true, command.IsValid);
    }
    [TestMethod]
    public void DadoUmDocumentNumberPequenoCommandDeveRetornarErro()
    {
        var _doc = "123.456.789-1";

        var command = new CreateResidentCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, _doc, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmDocumentNumberGrandeCommandDeveRetornarErro()
    {
        var _doc = "123.456.789-123";

        var command = new CreateResidentCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, _doc, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmDocumentNumberCorretoEUmDocumentTypeInválidoCommandDeveRetornarErro()
    {
        var command = new CreateResidentCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCNPJ, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmDocumentNumber_2_CorretoEUmDocumentTypeInválidoCommandDeveRetornarErro()
    {
        var command = new CreateResidentCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCNPJ, Apart, Block, Password);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmCommandVálidoCommandDeveRetornarTrue()
    {
        var command = new CreateResidentCommand(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Apart, Block, Password);
        Assert.AreEqual(true, command.IsValid);
    }




    //Change name

    [TestMethod]
    public void DadoUmFirstNamePequenoChangeNameCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new ChangeNameResidentCommand(_name, LastName, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmFirstNameVazioChangeNameCommandDeveRetornarErro()
    {
        var command = new ChangeNameResidentCommand("  ", LastName, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmFirstNameMinimoChangeNameDeveRetornarTrue()
    {
        var _name = "Lou";

        var command = new ChangeNameResidentCommand(_name, LastName, Id);
        Assert.AreEqual(true, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNamePequenoChangeNameCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new ChangeNameResidentCommand(FirstName, _name, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNameVazioChangeNameCommandDeveRetornarErro()
    {
        var command = new ChangeNameResidentCommand(FirstName, "   ", Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNameMinimoChangeNameDeveRetornarTrue()
    {
        var _name = "Lou";

        var command = new ChangeNameResidentCommand(FirstName, _name, Id);
        Assert.AreEqual(true, command.IsValid);
    }
    [TestMethod]
    public void DadoUmIdInválidoChangeNameCommandDeveRetornarErro()
    {
        var command = new ChangeNameResidentCommand(FirstName, LastName, Guid.Empty);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoCommandVálidoChangeNameCommandDeveRetornarTrue()
    {
        var command = new ChangeNameResidentCommand(FirstName, LastName, Id);
        Assert.AreEqual(true, command.IsValid);
    }



    //ChangeDocs

    [TestMethod]
    public void DadoTypeVálidoEUmDocumentNumberInválidoChangeDocumentCommandDeveRetornarErro()
    {
        var command = new ChangeDocumentResidentCommand(DocumentTypeCPF, DocumentNumberCNPJ, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoTypeInválidoEUmDocumentNumberVálidoChangeDocumentCommandDeveRetornarErro2()
    {
        var command = new ChangeDocumentResidentCommand(DocumentTypeCNPJ, DocumentNumberCPF, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmIdInválidoChangeDocumentCommandDeveRetornarErro()
    {
        var command = new ChangeDocumentResidentCommand(DocumentTypeCNPJ, DocumentNumberCPF, Guid.Empty);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoCommandVálidoChangeDocumentCommandDeveRetornarTrue()
    {
        var command = new ChangeDocumentResidentCommand(DocumentTypeCPF, DocumentNumberCPF, Id);
        Assert.AreEqual(true, command.IsValid);
    }



    //ChangeEmail

    [TestMethod]
    public void DadoEmailInválidoChangeEmailCommandDeveRetornarErro()
    {
        //https://github.com/andrebaltieri/Flunt/blob/main/Flunt.Tests/EmailValidationTests.cs
        var _email = "aaaa@aaaaa";
        var command = new ChangeEmailResidentCommand(_email, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoIdInválidoChangeEmailCommandDeveRetornarErro()
    {
        var command = new ChangeEmailResidentCommand(Email, Guid.Empty);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoCommandVálidoChangeEmailCommandDeveRetornarTrue()
    {
        var command = new ChangeEmailResidentCommand(Email, Id);
        Assert.AreEqual(true, command.IsValid);
    }



    //ChangePhone

    [TestMethod]
    public void DadoNumeroPequenoChangePhoneCommandDeveRetornarErro()
    {
        var _number = "1234-1234";
        var command = new ChangePhoneNumberResidentCommand(_number, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmPhoneNumberGrandeChangePhoneDeveRetornarErro()
    {
        var _phone = "+55 012 9 1234-1234 5";

        var command = new ChangePhoneNumberResidentCommand(_phone, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoNumeroInválidoChangePhoneCommandDeveRetornarErro()
    {
        var _number = "4568a21347";
        var command = new ChangePhoneNumberResidentCommand(_number, Id);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoIdInválidoChangePhoneCommandDeveRetornarErro()
    {
        var command = new ChangePhoneNumberResidentCommand(PhoneNumber, Guid.Empty);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoCommandVálidoChangePhoneCommandDeveRetornarTrue()
    {
        var command = new ChangePhoneNumberResidentCommand(PhoneNumber, Id);
        Assert.AreEqual(true, command.IsValid);
    }
}