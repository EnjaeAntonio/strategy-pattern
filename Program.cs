
Client client = new User(20);

CommunityBadge communityBadge = new CommunityBadge(client);
Console.WriteLine($"Client Community Badge Reputation: {communityBadge.GetReputation()}");
Console.WriteLine($"Client Community Badge Priveleges: {communityBadge.GetPriveleges()}");
Console.WriteLine("----------");
BannedBadge bannedBadge = new BannedBadge(client);
Console.WriteLine($"Banned Badge Reputation: {bannedBadge.GetReputation()}");
Console.WriteLine($"Banned Badge Priveleges: {bannedBadge.GetPriveleges()}");
Console.WriteLine("----------");
HundredPostsBadge HundredPostsBadge = new HundredPostsBadge(client);
Console.WriteLine($"Hundred Posts Badge Reputation: {HundredPostsBadge.GetReputation()}");
Console.WriteLine($"Hundred Posts Badge Priveleges: {HundredPostsBadge.GetPriveleges()}");

public abstract class Client
{
    public abstract int GetReputation();
    public abstract string GetPriveleges();
}

public class User : Client
{
    private int _reputation = 0;
    public override string GetPriveleges()
    {
        string _grantBasicAccess()
        {
            return "User now has basic access!";
        }
        return _grantBasicAccess();
    }

    public User(int reputation)
    {
        _reputation = reputation;
    }

    public override int GetReputation()
    {
        return _reputation;
    }

}

public abstract class BadgeDecorator : Client 
{
    protected Client _client;
    public override string GetPriveleges()
    {
        _client.GetPriveleges();
        return "Basic Badge priveleges";
    }
    public override int GetReputation()
    {
        return _client.GetReputation();
    }

    public BadgeDecorator(Client client)
    {
        _client = client;
    }
}
public class CommunityBadge : BadgeDecorator
{
    public CommunityBadge(Client client) : base(client)
    {
    }

    public override string GetPriveleges()
    {
        _client.GetPriveleges();
        string _grantBasicAccess()
        {
            return "User now has basic access!";
        }
        return _grantBasicAccess();
    }


    public override int GetReputation()
    {
        return _client.GetReputation() + 5;
    }
}
public class BannedBadge : BadgeDecorator
{
    public BannedBadge(Client client) : base(client)
    {
    }

    public override string GetPriveleges()
    {
        _client.GetPriveleges();
        string _grantBasicAccess()
        {
            return "Access Denied!";
        }
        return _grantBasicAccess();
    }


    public override int GetReputation()
    {
        return _client.GetReputation() * 0;
    }
}
public class HundredPostsBadge : BadgeDecorator
{
    public HundredPostsBadge(Client client) : base(client)
    {
    }

    public override string GetPriveleges()
    {
        return _client.GetPriveleges();
    }

    public override int GetReputation()
    {
       return _client.GetReputation() + 100;
    }
}
