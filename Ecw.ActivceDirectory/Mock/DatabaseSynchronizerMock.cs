using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Ecw.ActiveDirectory.Mock
{
    // Mock lässt sich benutzen mit
    // ISynchronizer synchronizer = new DatabaseSynchronizerMock();
    // ads.SynchronizeEntities(synchronizer);
    public class DatabaseSynchronizerMock : IBackendSynchronizer
    {
        private Dictionary<Guid, string> groups = new Dictionary<Guid, string>();
        private Dictionary<Guid, List<Guid>> user_in_groups = new Dictionary<Guid, List<Guid>>();
        private Dictionary<Guid, List<Guid>> user_direct_assigned = new Dictionary<Guid, List<Guid>>();
        private List<Guid> userInGroupA, userInGroupB;
        private Guid direktZugewiesenerBenutzer;

        public DatabaseSynchronizerMock()
        {
            Guid groupA = System.Guid.NewGuid();
            Guid groupB = System.Guid.NewGuid();
            Guid userCST = new Guid("0c3201b8-26de-43f9-afdd-19411cf846f9"); // CST
            Guid userSNI = new Guid("ffd696d1-b4d3-4e9b-8334-a3ea5cd87cf7"); // SNI
            Guid userHZE = new Guid("91aaa9ea-9acf-40ce-9602-955ac19335b6"); // HZE
            direktZugewiesenerBenutzer = new Guid("da5fe1d2-bd85-4b47-96bf-644008d9c2f4"); // Technik

            groups.Add(groupA, "Gruppe A");
            groups.Add(groupB, "Gruppe B");

            userInGroupA = new List<Guid> { userCST };
            userInGroupB = new List<Guid> { userSNI, userHZE };

            user_in_groups.Add(groupA, userInGroupA);
            user_in_groups.Add(groupB, userInGroupB);
        }

        public IEnumerable<Guid> GetRegisteredGroups()
        {
            // Kopie zurückliefern
            return new List<Guid>(groups.Keys);
        }

        public IEnumerable<Guid> GetRegisteredUsers()
        {
            List<Guid> r = new List<Guid>();
            r.AddRange(userInGroupA);
            r.AddRange(userInGroupB);
            r.Add(direktZugewiesenerBenutzer);

            return r;
        }

        public void NotifyNonExistingGroup(Guid guid)
        {
            Console.WriteLine();
            Console.WriteLine("Gruppe {0} existiert nicht im Active Directory.", guid);
            Console.WriteLine("  - Lösche Zuweisungen zwischen Gruppe und Benutzer");
            List<Guid> guidsInGroup = null;

            user_in_groups.TryGetValue(guid, out guidsInGroup);

            guidsInGroup.Clear();
            user_in_groups.Remove(guid);
            Console.WriteLine("  - Lösche Gruppe");
            groups.Remove(guid);
        }

        public void NotifyNonExistingUser(Guid guid)
        {
            Console.WriteLine();
            Console.WriteLine("Der Benutzer {0} wurde nicht im Active Directory gefunden.", guid);

            if (userInGroupA.Contains(guid) || userInGroupB.Contains(guid))
            {
                Console.WriteLine("  - Lösche Benutzer aus Gruppenzuweisung");
                userInGroupA.Remove(guid);
                userInGroupB.Remove(guid);
            }
        }

        public void NotifyUserFound(UserPrincipal userPrincipal, UserPrincipal groupPrincipal)
        {
            Console.WriteLine();
            Console.WriteLine("Benutzer {0} ({1}) gefunden", userPrincipal.Name, userPrincipal.Guid);
            Console.WriteLine("  - Aktualisiere Benutzer in der Datenbank");

            if (groupPrincipal != null)
            {
                Console.WriteLine("  - Aktualisere Benutzergruppe");
            }
        }
    }
}
