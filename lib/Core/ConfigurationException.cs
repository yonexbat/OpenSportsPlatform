using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Core
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string configkey) :  base($"Configuration {configkey} must not be null.")
        {

        }
    }
}
