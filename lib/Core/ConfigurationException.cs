namespace OpenSportsPlatform.Lib.Core;

public class ConfigurationException : Exception
{
    public ConfigurationException(string configkey) :  base($"Configuration {configkey} must not be null.")
    {

    }
}