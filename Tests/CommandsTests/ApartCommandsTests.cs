using Domain.Commands.Apart;
using Domain.Commands.Packages;
using Domain.Commands.Resident;
using Domain.Commands.Visitant;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.CommandsTests
{
    [TestClass]
    public class ApartCommandsTests
    {
        private readonly int Number = 241;
        private readonly int Block = 5;

        private string FirstName = "Matheus";
        private string LastName = "Ferreira";
        private string Email = "matheus.henrique123@gmail.com";
        private string PhoneNumber = "+55 (41) 9 1234-1234";
        private EDocumentType DocumentTypeCPF = EDocumentType.CPF;
        //private EDocumentType DocumentTypeCNPJ = EDocumentType.CNPJ;
        private string DocumentNumberCPF = "123.456.789-12";
        //private string DocumentNumberCNPJ = "67.573.684/0001-91";
        private bool Active = true;

        private readonly string BarCode = "9781234567897";
        private readonly string ItemName = "Battle Rifle";
        private readonly EPackageType Type = EPackageType.MediumPackage;
        private readonly string Addressee = "R. MyStreetName, MyCity, Earth";
        private readonly string Sender = "Noble Six";
        private readonly string SenderAddress = "Uplift Reserve, New Mombasa, Earth";

        private readonly CreateVisitantCommand visitantCommand;
        private readonly CreateResidentCommand residentCommand;
        private readonly CreatePackageCommand packageCommand;

        private readonly Guid Id = Guid.Parse("0ecdb2d6-0021-4c0f-8e4f-14080940715b");

        public ApartCommandsTests()
        {
            visitantCommand = new(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
            residentCommand = new(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF);
            packageCommand = new(BarCode, Type, Addressee, Sender, SenderAddress, ItemName);
        }



        //CreateCommand

        [TestMethod]
        public void DadoUmBlockInválidoCommandDeveRetornarErro()
        {
            int _block = 0;
            var command = new CreateApartCommand(Number, _block);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmNumberInválidoCommandDeveRetornarErro()
        {
            int _number = 0;
            var command = new CreateApartCommand(_number, Block);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmCreateApartCommandVálidoCommandDeveRetornarTrue()
        {
            var command = new CreateApartCommand(Number, Block);
            Assert.AreEqual(true, command.IsValid);
        }



        //AddVisitant

        [TestMethod]
        public void DadoUmBlockInválidoAddVisitantCommandDeveRetornarErro()
        {
            int _block = 0;
            var command = new AddVisitantToApartCommand(Number, _block, visitantCommand);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmNumberInválidoAddVisitantCommandDeveRetornarErro()
        {
            int _number = 0;
            var command = new AddVisitantToApartCommand(_number, Block, visitantCommand);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoVisitantCommandInválidoCommandDeveRetornarErro()
        {
            CreateVisitantCommand _visitantCommand = new(" ", LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);

            var command = new AddVisitantToApartCommand(Number, Block, _visitantCommand);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmAddVisitantCommandVálidoCommandDeveRetornarTrue()
        {
            var command = new AddVisitantToApartCommand(Number, Block, visitantCommand);
            Assert.AreEqual(true, command.IsValid);
        }



        //AddResident

        [TestMethod]
        public void DadoUmBlockInválidoAddResidentCommandDeveRetornarErro()
        {
            int _block = 0;
            var command = new AddResidentToApartCommand(Number, _block, residentCommand);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmNumberInválidoAddResidentCommandDeveRetornarErro()
        {
            int _number = 0;
            var command = new AddResidentToApartCommand(_number, Block, residentCommand);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmAddResidentCommandInválidoCommandDeveRetornarErro()
        {
            CreateResidentCommand _residentCommand = new(" ", LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF);

            var command = new AddResidentToApartCommand(Number, Block, _residentCommand);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmAddResidentCommandVálidoCommandDeveRetornarTrue()
        {
            var command = new AddResidentToApartCommand(Number, Block, residentCommand);
            Assert.AreEqual(true, command.IsValid);
        }



        //AddPackage

        [TestMethod]
        public void DadoUmBlockInválidoAddPackageCommandDeveRetornarErro()
        {
            int _block = 0;
            var command = new AddPackageToApartCommand(Number, _block, packageCommand);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmNumberInválidoAddPackageCommandDeveRetornarErro()
        {
            int _number = 0;
            var command = new AddPackageToApartCommand(_number, Block, packageCommand);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmAddpackageCommandInválidoCommandDeveRetornarErro()
        {
            CreatePackageCommand _package = new("", Type, Addressee, Sender, SenderAddress, ItemName);

            var command = new AddPackageToApartCommand(Number, Block, _package);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmAddpackageCommandVálidoCommandDeveRetornarTrue()
        {
            var command = new AddPackageToApartCommand(Number, Block, packageCommand);
            Assert.AreEqual(true, command.IsValid);
        }



        //DeleteResident

        [TestMethod]
        public void DadoUmBlockInválidoDeleteResidentCommandDeveRetornarErro()
        {
            int _block = 0;
            var command = new DeleteResidentFromApartCommand(Number, _block, Id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmNumberInválidoDeleteResidentCommandDeveRetornarErro()
        {
            int _number = 0;
            var command = new DeleteResidentFromApartCommand(_number, Block, Id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DaduUmIdInválidoDeleteResidentCommandDeveRetornarErro()
        {
            var _id = Guid.Empty;
            var command = new DeleteResidentFromApartCommand(Number, Block, _id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmDeleteResidentCommandVálidoCommandDeveRetornarTrue()
        {
            var command = new DeleteResidentFromApartCommand(Number, Block, Id);
            Assert.AreEqual(true, command.IsValid);
        }



        //DeleteVisitant

        [TestMethod]
        public void DadoUmBlockInválidoDeleteVisitantCommandDeveRetornarErro()
        {
            int _block = 0;
            var command = new DeleteVisitantFromApartCommand(Number, _block, Id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmNumberInválidoDeleteVisitantCommandDeveRetornarErro()
        {
            int _number = 0;
            var command = new DeleteVisitantFromApartCommand(_number, Block, Id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DaduUmIdInválidoDeleteVisitantCommandDeveRetornarErro()
        {
            var _id = Guid.Empty;
            var command = new DeleteVisitantFromApartCommand(Number, Block, _id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmDeleteVisitantCommandVálidoCommandDeveRetornarTrue()
        {
            var command = new DeleteVisitantFromApartCommand(Number, Block, Id);
            Assert.AreEqual(true, command.IsValid);
        }


        //DeletePackage

        [TestMethod]
        public void DadoUmBlockInválidoDeletePackageCommandDeveRetornarErro()
        {
            int _block = 0;
            var command = new DeletePackageFromApartCommand(Number, _block, Id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmNumberInválidoDeletePackageCommandDeveRetornarErro()
        {
            int _number = 0;
            var command = new DeletePackageFromApartCommand(_number, Block, Id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DaduUmIdInválidoDeletePackageCommandDeveRetornarErro()
        {
            var _id = Guid.Empty;
            var command = new DeletePackageFromApartCommand(Number, Block, _id);
            Assert.AreEqual(false, command.IsValid);
        }
        [TestMethod]
        public void DadoUmDeletePackageCommandVálidoCommandDeveRetornarTrue()
        {
            var command = new DeletePackageFromApartCommand(Number, Block, Id);
            Assert.AreEqual(true, command.IsValid);
        }
    }
}