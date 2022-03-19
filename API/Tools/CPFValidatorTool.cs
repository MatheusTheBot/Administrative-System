namespace API.Tools;
public static class CPFValidatorTool
{
    //Validation method reference: https://dicasdeprogramacao.com.br/algoritmo-para-validar-cpf/

    public static bool Validate(List<int> CPF)
    {
        List<int> ValidationsDigits = new List<int>();
        List<int> CPFDigits = new List<int>();
        bool Response = false;

        if (CPF.All(x => x == CPF.First()))
            return Response;

        for (int i = 1; i < 9; i++)
        {
            CPFDigits.Add(CPF[i]);
        }
        for (int i = 10; i < 11; i++)
        {
            ValidationsDigits.Add(CPF[i]);
        }

        int Counter = 10;
        int Result = 0;
        foreach (int i in CPFDigits)
        {
            Result = Result += (i * Counter);
            Counter--;
        }

        if ((Result * 10) % 11 == ValidationsDigits[0])
        {
            Response = true;
        }
        else
            Response = false;

        int SecondCounter = 11;
        int SecondResult = 0;
        foreach (int i in CPFDigits)
        {
            SecondResult = SecondResult += (i * Counter);
            Counter--;
        }
        SecondResult = SecondResult += ValidationsDigits[0] * SecondCounter;

        if ((SecondResult * 10) % 11 == ValidationsDigits[1])
        {
            Response = true;
        }
        else
            Response = false;
        return Response;
    }
    public static bool Validate(string CPF)
    {
        List<int> ValidationsDigits = new List<int>();
        List<int> CPFDigits = new List<int>();
        bool Response = false;

        if (CPF.All(x => x == CPF.First()))
            return Response;

        for (int i = 1; i < 9; i++)
        {
            CPFDigits.Add(CPF[i]);
        }
        for (int i = 10; i < 11; i++)
        {
            ValidationsDigits.Add(CPF[i]);
        }

        int Counter = 10;
        int Result = 0;
        foreach (int i in CPFDigits)
        {
            Result = Result += (i * Counter);
            Counter--;
        }

        if ((Result * 10) % 11 == ValidationsDigits[0])
        {
            Response = true;
        }
        else
            Response = false;

        int SecondCounter = 11;
        int SecondResult = 0;
        foreach (int i in CPFDigits)
        {
            SecondResult = SecondResult += (i * Counter);
            Counter--;
        }
        SecondResult = SecondResult += ValidationsDigits[0] * SecondCounter;

        if ((SecondResult * 10) % 11 == ValidationsDigits[1])
        {
            Response = true;
        }
        else
            Response = false;
        return Response;
    }
}