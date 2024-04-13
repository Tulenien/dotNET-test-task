namespace app.Configuration;

public sealed class ConfigurationSingleton
{
    public static Dictionary<string, string> _args { get; private set; }
    private const string _inputArgName = "--file-log";
    private const string _outputArgName = "--file-output";
    private const string _addressStartArgName = "--address-start";
    private const string _addressEndArgName = "--address-mask";
    private const string _timeIntervalStartArgName = "--time-start";
    private const string _timeIntervalEndArgName = "--time-end";
    private ConfigurationSingleton()
    {
        _args = new Dictionary<string, string>
        {
            { _inputArgName, "input file path" },
            { _outputArgName, "output file path" },
            { _addressStartArgName, "starting IPv4 address" },
            { _addressEndArgName, "decimal number for subnet constraint" },
            { _timeIntervalStartArgName, "time interval start" },
            { _timeIntervalEndArgName, "time interval end" }
        };
    }

    private static ConfigurationSingleton? instance = null;
    public static ConfigurationSingleton Instance
    {
        get
        {
            instance ??= new ConfigurationSingleton();
            return instance;
        }
    }

    public static bool ParseArgs(ref string[] args)
    {
        bool status = true;
        short inputFileCheck = 0;
        short outputFileCheck = 0;
        short addressStartCheck = 0;
        short addressEndCheck = 0;
        short intervalStartCheck = 0;
        short intervalEndCheck = 0;
        int size = args.Length;
        if (size % 2 != 0 || size > _args.Count)
        {
            throw new ArgumentException("Arguments input incorrect");
        }
        for (int i = 0; i < size; i += 2)
        {
            string key = args[i];
            if (_args.TryGetValue(key, out _))
            {
                status = false;
                break;
            }
            switch (key)
            {
                case _inputArgName:
                    inputFileCheck++; break;
                case _outputArgName:
                    outputFileCheck++; break;
                case _addressStartArgName:
                    addressStartCheck++; break;
                case _addressEndArgName:
                    addressEndCheck++; break;
                case _timeIntervalStartArgName:
                    intervalStartCheck++; break;
                case _timeIntervalEndArgName:
                    intervalEndCheck++; break;
                default:
                    break;
            }
            _args[key] = args[i + 1];
        }
        if (inputFileCheck != 1 || outputFileCheck != 1 || 
            addressStartCheck > 1 || addressEndCheck > 1 ||
            intervalStartCheck != 1 || intervalEndCheck != 1 ||
            (addressEndCheck > 0 && addressStartCheck < 1) ||
            (_args[_inputArgName] == _args[_outputArgName]))
        {
            status = false;
        }
        return status;
    }
}
