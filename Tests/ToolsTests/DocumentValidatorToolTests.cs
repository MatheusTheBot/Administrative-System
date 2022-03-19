using API.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ToolsTests;

[TestClass]
public class DocumentValidatorToolTests
{
    //generated data in https://www.4devs.com.br/gerador_de_cnpj and https://www.4devs.com.br/gerador_de_cpf
    private readonly string ValidCPF = "32839558084";
    private readonly string ValidCNPJ = "49629867000108";

    [TestMethod]
    public void DadoUmCPFVálidoToolDeveRetornarTrue()
    {
        var result = DocumentValidatorTool.CPF(ValidCPF);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void DadoUmCNPJVálidoToolDeveRetornarTrue()
    {
        var result = DocumentValidatorTool.CNPJ(ValidCNPJ);
        Assert.IsTrue(result);
    }
}