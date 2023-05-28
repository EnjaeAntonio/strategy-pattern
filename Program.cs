public abstract class Client
{
    public abstract int GetReputation();
    public abstract void GetPriveleges();
}

public class User : Client
{
    private int _reputation = 0;
    public override void GetPriveleges()
    {
        void _grantBasicAccess()
        {
            Console.WriteLine("User now has basic access wooh!");
        }
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
    public override void GetPriveleges()
    {
        _client.GetPriveleges();
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

    public override void GetPriveleges()
    {
        void _grantBasicAccess()
        {

        }
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

    public override void GetPriveleges()
    {
        throw new NotImplementedException();
    }

    public override int GetReputation()
    {
        throw new NotImplementedException();
    }
}
public class HundredPostsBadge : BadgeDecorator
{
    public HundredPostsBadge(Client client) : base(client)
    {
    }

    public override void GetPriveleges()
    {
        throw new NotImplementedException();
    }

    public override int GetReputation()
    {
        throw new NotImplementedException();
    }
}
