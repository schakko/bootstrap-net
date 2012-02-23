using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecw.Util.Configuration
{
    public class MachineDependentConfigurationManager
    {
        private string hostname;
        
        private Dictionary<string, MachineDependentConfiguration> configurations = new Dictionary<string, MachineDependentConfiguration>();

        public MachineDependentConfigurationManager()
        {
            hostname = System.Net.Dns.GetHostName().ToLower();
        }

        public void Register(MachineDependentConfiguration configuration)
        {
            configurations.Add(configuration.ConfigurationForMachine.ToLower(), configuration);
        }

        public MachineDependentConfiguration GetActiveConfiguration()
        {
            if (!configurations.ContainsKey(hostname))
            {
                return new MachineDependentConfiguration("default");
            }

            return configurations[hostname];
        }
    }

    public class MachineDependentConfiguration
    {
        private Dictionary<string, string> configuration = new Dictionary<string, string>();

        public string ConfigurationForMachine { get; set;}

        public Object IndividualConfiguration { get; set; }

        public MachineDependentConfiguration(string configurationForMachine)
        {
            ConfigurationForMachine = configurationForMachine;
        }

        public string Get(string key)
        {
            if (configuration.ContainsKey(key)) 
            {
                return configuration[key];
            }

            return null;
        }

        public void Set(string key, string value)
        {
            configuration[key] = value;
        }
    }
}
