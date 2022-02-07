using Domain.Commands;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CommandsTests;

[TestClass]
public class ResidentCommandsTest
{
    private string FirstName = "Matheus";
    private string LastName = "Ferreira";
    private string Email = "matheus.henrique123@gmail.com";
    private string PhoneNumber = "(41) 9 1234-1234";
    private EDocumentType DocumentType = EDocumentType.CPF;
    private string DocumentNumber = "123.456.789-12";
    private string DocumentNumber2 = "67.573.684/0001-91";

    public ResidentCommandsTest()
    {
        //correctCommand = new CreateResidentCommand(FirstName, LastName, Email, PhoneNumber, DocumentType, DocumentNumber);
    }

    //Red, Green, Refactor

    [TestMethod]
    public void DadoUmFirstNamePequenoCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new CreateResidentCommand(_name, LastName, Email, PhoneNumber, DocumentType, DocumentNumber);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmFirstNameVazioCommandDeveRetornarErro()
    {
        var command = new CreateResidentCommand("    ", LastName, Email, PhoneNumber, DocumentType, DocumentNumber);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNamePequenoCommandDeveRetornarErro()
    {
        string _name = "ab";
        var command = new CreateResidentCommand(FirstName, _name, Email, PhoneNumber, DocumentType, DocumentNumber);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmLastNameVazioCommandDeveRetornarErro()
    {
        var command = new CreateResidentCommand(FirstName, "  ", Email, PhoneNumber, DocumentType, DocumentNumber);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmEmailVazioCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void DadoUmEmailErradoCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void DadoUmPhoneNumberPequenoCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void DadoUmPhoneNumberGrandeCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void DadoUmPhoneNumberErradoCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void DadoUmDocumentNumberPequenoCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void DadoUmDocumentNumberGrandeCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void DadoUmDocumentNumberCorretoEUmDocumentTypeInválidoCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void DadoUmDocumentNumber_2_CorretoEUmDocumentTypeInválidoCommandDeveRetornarErro()
    {
        Assert.Fail();
    }
}