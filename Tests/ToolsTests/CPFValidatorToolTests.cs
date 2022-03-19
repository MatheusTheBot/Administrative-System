using API.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests.ToolsTests;

[TestClass]
public class CPFValidatorToolTests
{
    //generated data in https://www.4devs.com.br/gerador_de_cnpj and https://www.4devs.com.br/gerador_de_cpf
    private readonly string ValidCPF = "32839558084";
    private readonly string ValidCNPJ = "49629867000108";

    [TestMethod]
    public void DadoUmCPFVálidoToolDeveRetornarTrue()
    {
        var result = CPFValidatorTool.Validate(ValidCPF);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void DadoUmCNPJVálidoToolDeveRetornarTrue()
    {
        var result = CNPJValidatorTool.Validate(ValidCNPJ);
        Assert.IsTrue(result);
    }
}