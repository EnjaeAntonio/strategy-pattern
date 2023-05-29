
public class Program
{
    public static void Main(string[] args)
    {
        // Create a factory for generating clients in a system that doesn't require two-factor authentication
        ClientFactory falseClientFactory = new ClientFactory(false);

        // Create a factory for generating clients in a system that does require two-factor authentication
        ClientFactory trueClientFactory = new ClientFactory(true);

        // Create a standard user client with Two Factor Authentication turned off
        Client userJames = falseClientFactory.CreateClient("James", "Password123", false, false, true);
        Console.WriteLine($"Hi I am a {userJames}");

        // Create an admin client with Two Factor Authentication turned off
        Client adminJohn = falseClientFactory.CreateClient("AdminJohn", "AdminPassword123", false, true, false);
        Console.WriteLine($"Hi I am an {adminJohn}");

        // Create an authorized user client with Two Factor Authentication turned on
        Client authorizedUserEnjae = trueClientFactory.CreateClient("Enjae", "AuthPassword123", true, false, true);
        Console.WriteLine($"Hi I am an {authorizedUserEnjae}");

        // Create a standard user client with Two Factor Authentication turned on
        Client userKinah = trueClientFactory.CreateClient("Kinah", "Password789", true, false, false);
        Console.WriteLine($"Hi I am an {userKinah}");

        try
        {
            // Attempt to create a standard user client with Two Factor Authentication turned off
            // in a system that requires two-factor authentication, 
            // this will throw an exception which we catch below
            Client userAntonio = trueClientFactory.CreateClient("Antonio", "Password456", false, false, true);
        }
        catch (Exception e)
        {
            Console.WriteLine("Caught expected exception: " + e.Message);
        }

        Console.WriteLine();
        Console.WriteLine("==== Passwords ====");
        Console.WriteLine($"User James hashed password: {userJames.PasswordHash()}");
        Console.WriteLine($"Admin Johns hashed password: {adminJohn.PasswordHash()}");
        Console.WriteLine($"Authorized User Enjae's hashed password: {authorizedUserEnjae.PasswordHash()}");
    }
}


public abstract class Client
{
    protected string UserName { get; set; }
    protected string Password { get; set; }
    public bool TwoFactorAuthentication { get; set; }
    public bool IsAdmin { get; set; }
    public abstract string PasswordHash();

    public Client (string userName, string password, bool twoFactorAuthentication, bool isAdmin)
    {
        UserName = userName;
        Password = password;
        TwoFactorAuthentication = twoFactorAuthentication;
        IsAdmin = isAdmin;
        PasswordHash();
    }
}

public class User : Client
{
    public User(string userName, string password, bool twoFactorAuthentication, bool isAdmin) : base(userName, password, twoFactorAuthentication, isAdmin)
    {
    }

    public override string PasswordHash()
    {
        return $" asdgdfsfawwEDASd{Password}tre1321rSAF";
    }
}

public class AuthorizedUser : Client
{
    public AuthorizedUser(string userName, string password, bool twoFactorAuthentication, bool isAdmin) : base(userName, password, twoFactorAuthentication, isAdmin)
    {
    }

    public override string PasswordHash()
    {
        return $" asdgdfsfawwEDASd{Password}tre1321rSAF";
    }
}

public class Admin : Client
{
    public Admin(string userName, string password, bool twoFactorAuthentication, bool isAdmin) : base(userName, password, twoFactorAuthentication, isAdmin)
    {
    }

    public override string PasswordHash()
    {
        return $" asdgdfsfawwEDASd{Password}tre1321rSAF";
    }
}

public class ClientFactory
{
    private ClientHandler clientHandler;
    public ClientFactory(bool twoFactorRequired)
    {
        if(twoFactorRequired)
        {
            clientHandler = new TwoFactorRequired();
        }else
        {
            clientHandler = new TwoFactorNotRequired();
        }
    }

    public Client CreateClient(string name, string password, bool twoFactorAuthentication, bool isAdmin, bool twoFactorRequired)
    {
        return clientHandler.CreateClient(name, password, twoFactorAuthentication, isAdmin, twoFactorRequired);
    }

}
public abstract class ClientHandler
{
    protected ClientHandler Factory { get; set; }
    public abstract Client CreateClient(string name, string password, bool twoFactorAuthentication, bool isAdmin, bool twoFactorRequired);
}

public class TwoFactorRequired : ClientHandler
{
    public override Client CreateClient(string name, string password, bool twoFactorAuthentication, bool isAdmin, bool twoFactorRequired)
    {
        if (!twoFactorAuthentication && twoFactorRequired)
        {
            throw new Exception("You must have Two Factor Authentication enabled, or the factory must have a value of 'true'.");
        }

        if (isAdmin)
        {
            return new Admin(name, password, twoFactorAuthentication, isAdmin);
        }else if (twoFactorAuthentication)
        {
            return new AuthorizedUser(name, password, twoFactorAuthentication, isAdmin);
        }
        else
        {
            return new User(name, password, twoFactorAuthentication, isAdmin);
        }
    }
}

public class TwoFactorNotRequired : ClientHandler
{
    public override Client CreateClient(string name, string password, bool twoFactorAuthentication, bool isAdmin, bool twoFactorRequired)
    {

        if (isAdmin)
        {
            return new Admin(name, password, twoFactorAuthentication, isAdmin);
        }
        else
        {
            return new User(name, password, twoFactorAuthentication, isAdmin);
        }
    }
}