using System;

class Program
{
    static void Main(string[] args)
    {
        User user1 = new User("John Doe", "john@example.com", 25, false, 19);
        user1.HandleAccess(); // false

        Admin testAdmin = new Admin("Enjae Antonio", "enjae@gmail.com", 21, false);
        testAdmin.HandleAccess(); // True

        Manager testManager = new Manager("Jane Smith", "jane@example.com", 30, false);
        testManager.HandleAccess(); // True
    }
}

abstract class Client
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int? Age { get; set; }
    public bool AccessDisabled { get; set; }
    protected AccessHandler AccessHandler { get; set; }

    protected Client(string name, string email, int? age, bool accessDisabled, AccessHandler accessHandler)
    {
        Name = name;
        Email = email;
        Age = age;
        AccessDisabled = accessDisabled;
        AccessHandler = accessHandler;
    }

    public virtual void HandleAccess()
    {
        bool hasAccess = AccessHandler.GetAccess(Age, AccessDisabled);
        Console.WriteLine($"Access granted: {hasAccess}");
    }
}

class User : Client
{
    public int Reputation { get; set; }

    public User(string name, string email, int age, bool accessDisabled, int reputation)
        : base(name, email, age, accessDisabled, new HasReputation())
    {
        Reputation = reputation;
    }

    public override void HandleAccess()
    {
        bool hasAccess = AccessHandler.GetAccess(Reputation, AccessDisabled);
        Console.WriteLine($"Access granted: {hasAccess}");
    }
}

class Manager : Client
{
    public Manager(string name, string email, int age, bool accessDisabled)
        : base(name, email, age, accessDisabled, new HasAccessAutomatic())
    {
    }
}

class Admin : Client
{
    public Admin(string name, string email, int age, bool accessDisabled)
        : base(name, email, age, accessDisabled, new HasAccessAutomatic())
    {
    }
}

public interface AccessHandler
{
    bool GetAccess(int? reputation = 0, bool accessDisabled = false);
}

class HasReputation : AccessHandler
{
    public bool GetAccess(int? reputation = 0, bool accessDisabled = false)
    {
        if(accessDisabled == true || reputation >= 20)
        {
            return true;
        }
        return false;
    }
}

class HasAccessAutomatic : AccessHandler
{
    public bool GetAccess(int? reputation = 0, bool accessDisabled = false)
    {
        if(accessDisabled == false)
        {
            return true;
        }
        return false;
    }
}
