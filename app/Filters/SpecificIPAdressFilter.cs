using System.Net;
using System.Threading.Tasks;

namespace app.Filters;

public class SpecificIPAdressFilter : AbstractFilter
{
    private IPAddress _startAddress;
    private IPAddress _endAddress;
    private byte[] _startAddressBytes;
    private byte[] _endAddressBytes;

    public SpecificIPAdressFilter(string ipAddress)
    {
        _startAddress = IPAddress.Parse(ipAddress);
        _endAddress = _startAddress;
        byte[] ipAddressBytes = _startAddress.GetAddressBytes();
        _startAddressBytes = ipAddressBytes;
        _endAddressBytes = ipAddressBytes;
    }

    public SpecificIPAdressFilter(string ipAddress, int mask)
    {
        _startAddress = IPAddress.Parse(ipAddress);
        _endAddress = _startAddress;
        byte[] ipAddressBytes = _startAddress.GetAddressBytes();
        _startAddressBytes = ipAddressBytes;
        ipAddressBytes[3] = (byte)mask;
        _endAddressBytes = ipAddressBytes;
    }

    protected override bool Evaluate(object request)
    {
        IPAddress address = IPAddress.Parse(request as string);
        byte[] addressBytes = address.GetAddressBytes();
        bool result = true;
        for (int i = 0; i < 4; i++)
        {
            if (addressBytes[i] < _startAddressBytes[i] || addressBytes[i] > _endAddressBytes[i])
            {
                result = false;
                break;
            }
        }
        return result;
    }
}
