using System.Security.Cryptography;
using System.Text;

namespace API.Tools;
public static class PasswordTool
{
    public static string Encript(string password)
    {
        using SHA256 hasher = SHA256.Create();

        byte[] data = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));

        var result = new StringBuilder();

        // Loop through each byte of the hashed data
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            result.Append(data[i].ToString("x2"));
        }

        return result.ToString();
    }
    public static bool Verify(string hashedPassword, string password)
    {
        var PasswordToCompare = Encript(password);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.Ordinal;

        return comparer.Compare(PasswordToCompare, hashedPassword) == 0;
    }
}