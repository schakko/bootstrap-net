using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ecw.ActiveDirectory.Attributes;
using System.DirectoryServices.ActiveDirectory;

namespace Ecw.ActiveDirectory
{
    public enum BACKEND_TYPE { DATABASE, ACTIVE_DIRECTORY };
    public enum SUBJECT_TYPE { USER, GROUP };


    public class ActiveDirectorySearch : IBackendSearchProvider
    {
        PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

        private int clientTimeout = 30;
        public int ClientTimeout
        {
            get
            {
                return clientTimeout;
            }
            set
            {
                clientTimeout = value;
            }
        }
        private int serverTimeLimit = 30;
        public int ServerTimeLimit
        {
            get
            {
                return serverTimeLimit;
            }
            set
            {
                serverTimeLimit = value;
            }
        }

        private string userDisplayNameFormat = "{sn:Nachname}, {givenName:Vorname} ({department:Unbekannte Abteilung})";
        public string UserDisplayNameFormat
        {
            get
            {
                return userDisplayNameFormat;
            }
            set
            {
                userDisplayNameFormat = value;
            }
        }


        private string securityGroupDisplayNameFormat = "{cn:Unbekannter Sicherheitsgruppenname}";
        public string SecurityGroupDisplayNameFormat
        {
            get
            {
                return securityGroupDisplayNameFormat;
            }
            set
            {
                securityGroupDisplayNameFormat = value;
            }
        }

        private List<IAttributeSearch> attributeSearcher;
        public List<IAttributeSearch> AttributeSearcher
        {
            get
            {
                if (attributeSearcher == null)
                {
                    attributeSearcher = new List<IAttributeSearch>();
                }

                return attributeSearcher;
            }
            set
            {
                attributeSearcher = value;
            }
        }

        // TODO Externer Context o.ä.
        public ActiveDirectorySearch()
        {
            AttributeSearcher.Add(new StringAttributeSearch("sn"));
            AttributeSearcher.Add(new StringAttributeSearch("givenName"));
            AttributeSearcher.Add(new StringAttributeSearch("sAMAccountName"));
            AttributeSearcher.Add(new GuidAttributeSearch("objectGUID"));
        }

        public PrincipalSearchResult<Principal> FindAllMembersInGroup(string groupName)
        {
            GroupPrincipal group = FindGroup(groupName);
            return group.GetMembers(true);
        }

        public GroupPrincipal FindGroup(string groupName)
        {
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, groupName);

            return group;
        }

        public UserPrincipal FindUser(String userInfo)
        {
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userInfo);
            return user;
        }


        /// <summary>
        /// Sucht nach Gruppen
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<BackendModel> SearchForGroup(String searchString)
        {
            return Search(searchString, SUBJECT_TYPE.GROUP);
        }

        /// <summary>
        /// Sucht entweder nach Gruppe oder Person
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="subjectType"></param>
        /// <returns></returns>
        public IEnumerable<BackendModel> Search(String searchString, SUBJECT_TYPE subjectType)
        {
            List<BackendModel> r = new List<BackendModel>();
            
            string objectCategory = (SUBJECT_TYPE.GROUP.Equals(subjectType)) ? "group" : "person";

            if (searchString == null || searchString.Length == 0)
            {
                return r;
            }

            foreach (IAttributeSearch searcher in AttributeSearcher)
            {
                StringBuilder sb = new StringBuilder();
                string attributeSearchString = searcher.CreateAttributeSearcher(searchString);

                if (attributeSearchString == null || attributeSearchString.Length == 0)
                {
                    continue;
                }

                // Nach allen Benutzern im Kontext suchen und wichtig: die Wildcard-Attribute mit UND verknüpfen
                sb.Append("(&(objectCategory=");
                sb.Append(objectCategory);
                sb.Append(")(&");
                sb.Append(attributeSearchString);
                sb.Append("))");

                DirectoryEntry de = new DirectoryEntry("LDAP://" + ctx.ConnectedServer);
                DirectorySearcher ds = new DirectorySearcher(de, sb.ToString());
                ds = UpdateTimelimits(ds);
                
                foreach (SearchResult searchResult in ds.FindAll())
                {
                    BackendModel model = ToModel(searchResult.GetDirectoryEntry(), subjectType);

                    if (!r.Contains(model))
                    {
                        r.Add(model);
                    }
                }
            }

            return r;
        }

        /// <summary>
        /// Sucht ein oder mehrere Elemente im Acitve Directory mit dem angegebenen sAMAccountName
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <returns></returns>
        public IEnumerable<BackendModel> SearchByAccountName(string sAMAccountName)
        {
            StringBuilder sb = new StringBuilder();
            List<BackendModel> r = new List<BackendModel>(); 

            sb.Append("(&(objectCategory=");
            sb.Append("person");
            sb.Append(")(&");
            sb.Append("samaccountname=");
            sb.Append(sAMAccountName);
            sb.Append("))");

            DirectoryEntry de = new DirectoryEntry("LDAP://" + ctx.ConnectedServer);
            DirectorySearcher ds = new DirectorySearcher(de, sb.ToString());
            ds = UpdateTimelimits(ds);

            foreach (SearchResult searchResult in ds.FindAll())
            {
                BackendModel model = ToModel(searchResult.GetDirectoryEntry(), SUBJECT_TYPE.USER);

                if (!r.Contains(model))
                {
                    r.Add(model);
                }
            }

            return r;
        }

        /// <summary>
        /// Ändert die Timelimits anhand der ggb. Konfiguration
        /// </summary>
        /// <param name="directorySearcher"></param>
        /// <returns></returns>
        private DirectorySearcher UpdateTimelimits(DirectorySearcher directorySearcher)
        {
            if (ClientTimeout > 0)
            {
                directorySearcher.ClientTimeout = TimeSpan.FromSeconds(clientTimeout);
            }

            if (ServerTimeLimit > 0)
            {
                directorySearcher.ServerTimeLimit = new TimeSpan(0, 2, 30);
            }

            return directorySearcher;

        }

        /// <summary>
        /// Converts given object to backend model
        /// </summary>
        /// <param name="directoryEntry"></param>
        /// <param name="subjectType"></param>
        /// <returns></returns>
        public BackendModel ToModel(DirectoryEntry directoryEntry, SUBJECT_TYPE subjectType)
        {
            BackendModel r = new BackendModel();
            // als GUID wird die objectGUID benutzt, die wirklich einmalig ist
            byte[] guid = (byte[])GetPropertyFromDirectoryEntry(directoryEntry, "objectGUID");
            r.Guid = new Guid(guid);

            string format = (subjectType.Equals(SUBJECT_TYPE.GROUP)) ? (SecurityGroupDisplayNameFormat) : (UserDisplayNameFormat);

            r.DisplayName = FormatStringByDirectoryEntry(format, directoryEntry);
            r.BackendType = BACKEND_TYPE.ACTIVE_DIRECTORY;
            r.SubjectType = subjectType;
            r.DistinguishedName = (string)GetPropertyFromDirectoryEntry(directoryEntry, "distinguishedName");
            r.NativeObject = directoryEntry;

            return r;
        }

        /// <summary>
        /// Formatiert einen String nach dem gegebenen Format. Die Werte werden aus dem directoryEntry gelesen
        /// </summary>
        /// <param name="format">z.B. "Herr {name}, {sn} {department}" wird automatisch so aufgelöst, dass die passenden Attribute aus dem DirectoryEntry gelesen werden</param>
        /// <param name="directoryEntry"></param>
        /// <returns></returns>
        public string FormatStringByDirectoryEntry(string format, DirectoryEntry directoryEntry)
        {
            Regex regex = new Regex(@"{([^}]+)}", RegexOptions.IgnoreCase);

            string r = format;
            foreach (Match match in regex.Matches(format))
            {
                string property = (match.Value.Length > 2) ? (match.Value.Substring(1, (match.Value.Length - 2))) : (match.Value); 
                r = r.Replace(match.Value, GetStringFromDirectoryEntry(directoryEntry, property));
            }
            return r;
        }

        /// <summary>
        /// Liefert einen Eintrag aus dem Active Directory als String zurück. Es wird also automatisch in einen String gecastet.
        /// </summary>
        /// <param name="directoryEntry"></param>
        /// <param name="propertyName">z.B. "cn" oder "cn:Default-CN". Mit zweiterer Darstellung wird ein Default-Wert benutzt</param>
        /// <returns></returns>
        public string GetStringFromDirectoryEntry(DirectoryEntry directoryEntry, string propertyName)
        {
            string[] property = propertyName.Split(':');

            Object o = GetPropertyFromDirectoryEntry(directoryEntry, property[0]);
            string r = "";

            try
            {
                r = (string)o;
            }
            catch (InvalidCastException e)
            {
            }

            // Standardwert wurde angegeben
            if (((r == null) || (r.Length == 0)) && (property.Length > 1))
            {
                r = property[1];
            }

            return r;
        }

        /// <summary>
        /// Returns property from directory entry
        /// </summary>
        /// <param name="directoryEntry"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public Object GetPropertyFromDirectoryEntry(DirectoryEntry directoryEntry, String propertyName)
        {
            if (directoryEntry.Properties.Contains(propertyName) && (directoryEntry.Properties[propertyName].Count > 0))
            {
                PropertyValueCollection pc = directoryEntry.Properties[propertyName];
                return pc.Value;
            }

            return "";
        }

        private string attributeForDomainCommonName = "nCName";
        public string AttributeForDomainCommonName { get { return attributeForDomainCommonName; } set { attributeForDomainCommonName = value; } }
        private string netBiosRootDSE = "LDAP://RootDSE";
        public string NetBiosRootDSE { get { return netBiosRootDSE; } set { netBiosRootDSE = value; } }
        private string netBiosSearch = "(&(objectCategory=crossRef)(nETBIOSName={0}))";
        public string NetBiosSearch { get { return netBiosSearch; } set { netBiosSearch = value; } }


        /// <summary>
        /// Diese Methode sucht den passenden CN zu einem NetBIOS-Namen. Dies wird benötigt, wenn eine Person sich über Single-Sign-On anmelden will.
        /// Es wird dabei nämlich nur der NetBIOS-Name mitgeliefert und nicht der volle Domainname.
        /// </summary>
        /// <param name="netBiosName">String in der Form "DOMAIN"</param>
        /// <returns>Idealerweise dc=DOMAIN,dc=tld</returns>
        public string ResolveNetBiosDomainNameToCommonName(string netBiosDomainName)
        {
            string r = "";

            DirectoryEntry de = new DirectoryEntry(NetBiosRootDSE);
            string configDN = de.Properties["configurationNamingContext"].Value.ToString();

            de = new DirectoryEntry("LDAP://" + configDN);
            DirectorySearcher ds = new DirectorySearcher(de, String.Format(NetBiosSearch, netBiosDomainName));
            SearchResult sr = ds.FindOne();

            if (sr != null)
            {
                r = (string)GetPropertyFromDirectoryEntry(sr.GetDirectoryEntry(), AttributeForDomainCommonName);
            }

            return r;
        }

        /// <summary>
        /// Liefert den NetBIOS-Namen (DOMAIN\USERNAME => DOMAIN) zurück
        /// </summary>
        /// <param name="netBiosName"></param>
        /// <returns></returns>
        public string GetNetBiosDomainNameFromIdenty(string netBiosName)
        {
            int idxSeperator = netBiosName.IndexOf(@"\");
            string r = "";

            if (idxSeperator >= 0)
            {
                r = netBiosName.Substring(0, idxSeperator);
            }

            return r;
        }
        
        /// <summary>
        /// Liefert den Benutzernamen aus dem String zurück
        /// </summary>
        /// <param name="netBiosName"></param>
        /// <returns></returns>
        public string GetSamAccountNameNameFromIdenty(string netBiosName)
        {
            int idxSeperator = netBiosName.IndexOf(@"\");
            string r = netBiosName;

            if (idxSeperator >= 0)
            {
                r = netBiosName.Substring(++idxSeperator);
            }

            return r;
        }
    }
}
