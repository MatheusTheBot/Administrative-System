using Microsoft.AspNet.Identity;

namespace Domain.Services;
public static class ServicePassword
{
    public static string Encript(string password)
    {
        var hasher = new PasswordHasher();
        return hasher.HashPassword(password);
    }
    public static bool Verify( string hashedPassword,string password)
    {
        var hasher = new PasswordHasher();
        var x = hasher.VerifyHashedPassword(hashedPassword, password);
        if(x.ToString() == "Success")
        {
            return true;
        }
        return false;
    }
}