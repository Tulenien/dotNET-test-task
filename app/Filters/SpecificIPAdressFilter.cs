using System.Net;

namespace app.Filters;

public class SpecificIPAdressFilter : AbstractFilter
{
    private byte[] _startAddressBytes;
    private byte[] _endAddressBytes;
    private bool _masked = false;

    public SpecificIPAdressFilter(string ipAddress)
    {
        IPAddress _startAddress = IPAddress.Parse(ipAddress);
        byte[] ipAddressBytes = _startAddress.GetAddressBytes();
        _startAddressBytes = ipAddressBytes;
        _endAddressBytes = ipAddressBytes;
    }

    public SpecificIPAdressFilter(string ipAddress, int mask)
    {
        IPAddress _startAddress = IPAddress.Parse(ipAddress);
        byte[] ipAddressBytes = _startAddress.GetAddressBytes();
        _startAddressBytes = ipAddressBytes;
        ipAddressBytes[3] = (byte)mask;
        _endAddressBytes = ipAddressBytes;
        _masked = true;
    }

    protected override bool Evaluate(object request)
    {
        IPAddress address = IPAddress.Parse(request as string);
        byte[] addressBytes = address.GetAddressBytes();
        bool result = true;
        for (int i = 0; i < 4; i++)
        {
            if (addressBytes[i] < _startAddressBytes[i] || 
                (_masked && addressBytes[i] > _endAddressBytes[i]))
            {
                result = false;
                break;
            }
        }
        return result;
    }
}
