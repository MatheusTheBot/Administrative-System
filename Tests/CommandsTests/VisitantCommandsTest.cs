using Domain.Commands.Visitant;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CommandsTests;

[TestClass]
public class VisitantCommandsTest
{
    private string FirstName = "Matheus";
    private string LastName = "Ferreira";
    private string Email = "matheus.henrique123@gmail.com";
    private string PhoneNumber = "+55 (41) 9 1234-1234";
    private EDocumentType DocumentTypeCPF = EDocumentType.CPF;
    private EDocumentType DocumentTypeCNPJ = EDocumentType.CNPJ;
    private string DocumentNumberCPF = "123.456.789-12";
    private string DocumentNumberCNPJ = "67.573.684/0001-91";
    private bool Active = true;

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
}