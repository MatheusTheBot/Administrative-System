using Domain.Commands.Packages;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.CommandsTests;

[TestClass]
public class PackagesCommandsTests
{
    private readonly string BarCode = "9781234567897";
    private readonly string ItemName = "Battle Rifle";
    private readonly EPackageType Type = EPackageType.MediumPackage;
    private readonly string Addressee = "R. MyStreetName, MyCity, Earth";
    private readonly string Sender = "Noble Six";
    private readonly string SenderAddress = "Uplift Reserve, New Mombasa, Earth";

    private readonly Guid Id = Guid.Parse("0ecdb2d6-0021-4c0f-8e4f-14080940715b");


    //CreatePackages

    [TestMethod]
    public void DadoUmBarCodePequenoCommandDeveRetornarErro()
    {
        var _barcode = "12345678912";
        var command = new CreatePackageCommand(_barcode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmBarCodeGrandeCommandDeveRetornarErro()
    {
        var _barcode = "12345678912345";
        var command = new CreatePackageCommand(_barcode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmBarCodeVazioCommandDeveRetornarErro()
    {
        var _barcode = "    ";
        var command = new CreatePackageCommand(_barcode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmAddresseVazioCommandDeveRetornarErro()
    {
        var _addressee = "    ";
        var command = new CreatePackageCommand(BarCode, Type, _addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmSenderVazioCommandDeveRetornarErro()
    {
        var _sender = "    ";
        var command = new CreatePackageCommand(BarCode, Type, Addressee, _sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmSenderAddressVazioCommandDeveRetornarErro()
    {
        var _senderAdress = "    ";
        var command = new CreatePackageCommand(BarCode, Type, Addressee, Sender, _senderAdress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmItemNameVazioCommandDeveRetornarTrue()
    {
        var command = new CreatePackageCommand(BarCode, Type, Addressee, Sender, SenderAddress);
        Assert.AreEqual(true, command.IsValid);
    }
    [TestMethod]
    public void DadoUmCommandVálidoCommandDeveRetornarTrue()
    {
        var command = new CreatePackageCommand(BarCode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(true, command.IsValid);
    }



    //ChangePackageType

    [TestMethod]
    public void DadoUmIdVazioChangeTypeCommandDeveRetornarErro()
    {
        var command = new ChangePackageTypeCommand(Guid.Empty, Type);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmChangeTypeCommandVálidoCommandDeveRetornarTrue()
    {
        var command = new ChangePackageTypeCommand(Id, Type);
        Assert.AreEqual(true, command.IsValid);
    }



    //UpdatePackage

    [TestMethod]
    public void DadoUmIdInválidoUpdateCommandDeveRetornarErro()
    {
        var _id = Guid.Empty;
        var command = new UpdatePackageCommand(_id, BarCode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmBarCodePequenoUpdateCommandDeveRetornarErro()
    {
        var _barcode = "12345678912";
        var command = new UpdatePackageCommand(Id, _barcode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmBarCodeGrandeUpdateCommandDeveRetornarErro()
    {
        var _barcode = "12345678912345";
        var command = new UpdatePackageCommand(Id, _barcode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmBarCodeVazioUpdateCommandDeveRetornarErro()
    {
        var _barcode = "    ";
        var command = new UpdatePackageCommand(Id, _barcode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmAddresseVazioUpdateCommandDeveRetornarErro()
    {
        var _addressee = "    ";
        var command = new UpdatePackageCommand(Id, BarCode, Type, _addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmSenderVazioUpdateCommandDeveRetornarErro()
    {
        var _sender = "    ";
        var command = new UpdatePackageCommand(Id, BarCode, Type, Addressee, _sender, SenderAddress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmSenderAddressVazioUpdateCommandDeveRetornarErro()
    {
        var _senderAdress = "    ";
        var command = new UpdatePackageCommand(Id, BarCode, Type, Addressee, Sender, _senderAdress, ItemName);
        Assert.AreEqual(false, command.IsValid);
    }
    [TestMethod]
    public void DadoUmItemNameVazioUpdateCommandDeveRetornarTrue()
    {
        var command = new UpdatePackageCommand(Id, BarCode, Type, Addressee, Sender, SenderAddress);
        Assert.AreEqual(true, command.IsValid);
    }
    [TestMethod]
    public void DadoUmUpdatePackageCommandVálidoCommandDeveRetornarTrue()
    {
        var command = new UpdatePackageCommand(Id, BarCode, Type, Addressee, Sender, SenderAddress, ItemName);
        Assert.AreEqual(true, command.IsValid);
    }
}