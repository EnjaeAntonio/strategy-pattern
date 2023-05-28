public abstract class Client
{
    public abstract void GetReputation();
    public abstract void GetPriveleges();
}

public class User : Client
{

    private int _reputation { get; set; } 
    public override void GetPriveleges()
    {
        throw new NotImplementedException();
    }

    public override void GetReputation()
    {
        void _grantBasicAccess()
        {
            Console.WriteLine("User now has basic access wooh!");
        }
    }
}

public abstract class BadeDecorator : Client 
{
    protected Client _client;
    public abstract override void GetPriveleges();
    public abstract override void GetReputation();
    
}
public class CommunityBadge : BadeDecorator
{
    public override void GetPriveleges()
    {
        throw new NotImplementedException();
    }

    public override void GetReputation()
    {
        throw new NotImplementedException();
    }
}
public class BannedBadge : BadeDecorator
{
    public override void GetPriveleges()
    {
        throw new NotImplementedException();
    }

    public override void GetReputation()
    {
        throw new NotImplementedException();
    }
}
public class HundredPostsBadge : BadeDecorator
{
    public override void GetPriveleges()
    {
        throw new NotImplementedException();
    }

    public override void GetReputation()
    {
        throw new NotImplementedException();
    }
}
