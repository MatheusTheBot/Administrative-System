namespace API.Tools;
public static class DocumentValidatorTool
{
    //Validation method reference: https://dicasdeprogramacao.com.br/algoritmo-para-validar-cpf/
    private static readonly int[] VerificationsKeys = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    private static readonly int[] SecondVerificationsKeys = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

    public static bool CPF(string cpf)
    {
        List<int> CPF = cpf.ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

        List<int> ValidationsDigits = new List<int>();
        List<int> CPFDigits = new List<int>();
        bool Response = false;

        if (CPF.All(x => x == CPF.First()))
            return Response;

        for (int i = 0; i < 9; i++)
        {
            CPFDigits.Add(CPF[i]);
        }
        for (int i = 9; i <= 10; i++)
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

        if (Response == false)
            return Response;

        int SecondCounter = 11;
        int SecondResult = 0;
        foreach (int i in CPFDigits)
        {
            SecondResult = SecondResult += (i * SecondCounter);
            SecondCounter--;
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
    public static bool CNPJ(string cnpj)
    {
        List<int> CNPJ = cnpj.ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

        List<int> ValidationsDigits = new List<int>();
        List<int> CNPJDigits = new List<int>();
        bool Response = false;

        if (CNPJ.All(x => x == CNPJ.First()))
            return Response;

        for (int i = 0; i < 12; i++)
        {
            CNPJDigits.Add(CNPJ[i]);
        }
        for (int i = 12; i <= 13; i++)
        {
            ValidationsDigits.Add(CNPJ[i]);
        }

        int Result = 0;
        int Counter = 0;
        foreach (int i in CNPJDigits)
        {
            Result = Result += (i * VerificationsKeys[Counter]);
            Counter++;
        }

        if ((Result * 10) % 11 == ValidationsDigits[0] || (Result * 10) % 11 == 10)
        {
            Response = true;
        }
        else
            Response = false;

        if (Response == false)
            return Response;

        int SecondResult = 0;
        int SecondCounter = 0;
        foreach (int i in CNPJDigits)
        {
            SecondResult = SecondResult += (i * SecondVerificationsKeys[SecondCounter]);
            SecondCounter++;
        }
        SecondResult = SecondResult += ValidationsDigits[0] * SecondVerificationsKeys[12];

        if ((SecondResult * 10) % 11 == ValidationsDigits[1])
        {
            Response = true;
        }
        else
            Response = false;
        return Response;
    }
}