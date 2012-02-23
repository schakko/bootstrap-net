using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Ecw.Util.Configuration
{
    /// <summary>
    /// Hilfsklasse zum Injizieren der Datenbankverbindung anhand des Maschinen-Namens
    /// </summary>
    public class MachineDependentConnectionString
    {
        public string ConnectionString { 
            get {
                string hostname = System.Net.Dns.GetHostName();
                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[hostname.ToUpper()];

                if (connectionString == null)
                {
                    connectionString = ConfigurationManager.ConnectionStrings[0];
                }

                if (connectionString != null)
                {
                    return connectionString.ConnectionString;
                }

                return "noConnectionStringDefined";
            } 

            set {
            } 
        }
    }
}
