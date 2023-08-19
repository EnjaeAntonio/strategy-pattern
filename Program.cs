using System.Threading.Channels;

Hardware receiver = new Receivers();
Hardware radio = new Radios();
Hardware amplifier = new Amplifiers();

receiver.SetRadio(2);
Console.WriteLine($"Receiver Set Radio: {receiver.ReceiveRadioStation()}");

receiver.SetLine(3);
Console.WriteLine($"Receiver Set Line: {receiver.ReceiveLineInput()}");

amplifier.SetLine(3);
Console.WriteLine($"Amplifier Set Line: {amplifier.ReceiveLineInput()}");

amplifier.SetRadio(2);
Console.WriteLine($"Amplifier Set Line: {amplifier.ReceiveRadioStation()}");

radio.SetLine(3);
Console.WriteLine($"Radio Bluetooth Set Line: {radio.ReceiveLineInput()}");
public abstract class Hardware
{
    protected ReceiveRadio ReceiveRadio { get; set; }
    protected ReceiveLine ReceiveLine { get; set; }
    protected int _channel = 0;
    protected int Channel { get { return _channel; } set { _channel = value; } }
    protected abstract void SwitchRadio();
    protected abstract void HandleLines();
    public virtual void SetRadio(int channel)
    {

    }

    public virtual void SetLine(int channel)
    {

    }
    public string ReceiveLineInput()
    {
        HandleLines();
        return ReceiveLine.SwitchLines();
    }

    public string ReceiveRadioStation()
    {
        SwitchRadio();
        return ReceiveRadio.SwitchChannels();
    }
}
class Receivers : Hardware
{
    public Receivers()
    {

    }

    public override void SetLine(int channel)
    {
        Channel = channel;
        HandleLines();
    }


    public override void SetRadio(int channel)
    {
        Channel = channel;
        SwitchRadio();
    }
    protected override void SwitchRadio()
    {
        if (_channel == 1)
        {
            ReceiveRadio = new ReceiveAM();
        }
        else if (_channel == 2)
        {
            ReceiveRadio = new ReceiveFM();
        }
        else
        {
            ReceiveRadio = new ReceiveRadioNone();
        }
    }

    protected override void HandleLines()
    {
        if(_channel == 1)
        {
            ReceiveLine = new ReceiveLineIn();
        } else if (_channel == 2)
        {
            ReceiveLine = new ReceiveMM();
        } else if (_channel == 3)
        {
            ReceiveLine = new ReceiveBluetooth();
        }else
        {
            ReceiveLine  =new ReceiveLineNone();
        }
    }
}

class Amplifiers : Hardware
{
    protected override void SwitchRadio()
    {
        ReceiveRadio = new ReceiveRadioNone();
    }
    public override void SetLine(int channel)
    {
        Channel = channel;
        HandleLines();
    }
    protected override void HandleLines()
    {
        if (_channel == 1)
        {
            ReceiveLine = new ReceiveLineIn();
        }
        else if (_channel == 2)
        {
            ReceiveLine = new ReceiveMM();
        }
        else if (_channel == 3)
        {
            ReceiveLine = new ReceiveBluetooth();
        }
        else
        {
            ReceiveLine = new ReceiveLineNone();
        }
    }
}

class Radios : Hardware
{

    public Radios()
    {

    }

    public override void SetRadio(int channel)
    {
        Channel = channel;
        SwitchRadio();
    }

    protected override void SwitchRadio()
    {
        if (_channel == 1)
        {
            ReceiveRadio = new ReceiveAM();
        }
        else if (_channel == 2)
        {
            ReceiveRadio = new ReceiveFM();
        }
        else
        {
            ReceiveRadio = new ReceiveRadioNone();
        }
    }

    public override void SetLine(int channel)
    {
        ReceiveLine = new ReceiveLineNone();
    }

    protected override void HandleLines()
    {
        ReceiveLine = new ReceiveLineNone();
    }
}



public interface ReceiveRadio
{
    string SwitchChannels();
}

class ReceiveFM : ReceiveRadio
{
    public string SwitchChannels()
    {
        return "This is the FM Channel";
    }
}

class ReceiveAM : ReceiveRadio
{
    public string SwitchChannels()
    {
        return "This is the AM Channel";

    }
}

class ReceiveRadioNone : ReceiveRadio
{
    public string SwitchChannels()
    {
        return "Access Denied";

    }
}

public interface ReceiveLine
{
    string SwitchLines();
}

class ReceiveLineIn : ReceiveLine
{
    public string SwitchLines()
    {
        return $"LineIn connected.";
    }
}

class ReceiveMM : ReceiveLine
{
    public string SwitchLines()
    {
        return $"MM connected.";

    }
}

class ReceiveBluetooth : ReceiveLine
{
    public string SwitchLines()
    {
        return $"Bluetooth connected.";
    }
}

class ReceiveLineNone : ReceiveLine
{
    public string SwitchLines()
    {
        return $"Access Denied.";
    }
}
