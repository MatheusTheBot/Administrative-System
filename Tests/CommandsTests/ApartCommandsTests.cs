using Domain.Commands.Apart;
using Domain.Commands.Packages;
using Domain.Commands.Resident;
using Domain.Commands.Visitant;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        private readonly CreateVisitantCommand visitantCommand;
        private readonly CreateResidentCommand residentCommand;
        private readonly CreatePackageCommand packageCommand;
        public ApartCommandsTests()
        {
            visitantCommand = new(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF, Active);
            residentCommand = new(FirstName, LastName, Email, PhoneNumber, DocumentTypeCPF, DocumentNumberCPF);
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


    }
}