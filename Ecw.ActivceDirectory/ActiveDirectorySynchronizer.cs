using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ecw.ActiveDirectory;

namespace Ecw.ActiveDirectory
{
    public class ActiveDirectorySynchronizer
    {
        private ActiveDirectorySearch activeDirectorySearch;

        public ActiveDirectorySynchronizer(ActiveDirectorySearch activeDirectorySearch)
        {
            this.activeDirectorySearch = activeDirectorySearch;
        }

        public void SynchronizeEntities(IBackendSynchronizer synchronizer)
        {
            IEnumerable<Guid> lookupElements = synchronizer.GetRegisteredGroups();
            // Enthält die GUIDs aller Benutzer, die während der Synchronisierung geupdatet worden sind
            List<Guid> updatedUserGuids = new List<Guid>();
            List<Guid> nonExistingGroups = new List<Guid>();
            Dictionary<Guid, List<Guid>> allUserInGroups = new Dictionary<Guid, List<Guid>>();

            // zuerst über alle Sicherheitsgruppen iterieren
            foreach (var guidOfGroup in lookupElements)
            {
                // Elemente innerhalb des System aktualisieren
                // Gruppe im Active Directory suchen
                GroupPrincipal groupInActiveDirectory = activeDirectorySearch.FindGroup(guidOfGroup.ToString());

                // Gruppe existiert nicht mehr im Active Directory, also muss der Synchronizer darüber informiert werden und ggf. die Gruppe löschen
                if (groupInActiveDirectory == null)
                {
                    nonExistingGroups.Add(guidOfGroup);
                    continue;
                }

                synchronizer.NotifyGroupFound(activeDirectorySearch.ToModel((DirectoryEntry)groupInActiveDirectory.GetUnderlyingObject(), SUBJECT_TYPE.GROUP));

                // Alle Benutzer (inkl. Benutzer in verschachtelten Benutzergruppen) suchen
                PrincipalSearchResult<Principal> usersInGroup = activeDirectorySearch.FindAllMembersInGroup(guidOfGroup.ToString());

                allUserInGroups.Add(groupInActiveDirectory.Guid.Value, new List<Guid>());

                foreach (var user in usersInGroup)
                {
                    BackendModel userBackendModel =  activeDirectorySearch.ToModel((DirectoryEntry)user.GetUnderlyingObject(), SUBJECT_TYPE.USER);
                    synchronizer.NotifyUserFound(userBackendModel);
                    updatedUserGuids.Add(userBackendModel.Guid);
                    allUserInGroups[groupInActiveDirectory.Guid.Value].Add(userBackendModel.Guid);
                }
            }


            // über alle nicht mehr existierenden Gruppen iterieren
            synchronizer.NotifyNonExistingGroups(nonExistingGroups);

            // nun über alle registrierten Benutzer iterieren
            lookupElements = synchronizer.GetRegisteredUsers();

            foreach (var guidOfUser in lookupElements)
            {
                UserPrincipal userInActiveDirectory = activeDirectorySearch.FindUser(guidOfUser.ToString());

                if (userInActiveDirectory == null)
                {
                    continue;
                }

                // Wenn der Benutzer nicht bereits bei der Verarbeitung der Gruppen angefasst worden ist, nun noch einmal aktualisieren
                if (!updatedUserGuids.Contains((Guid)userInActiveDirectory.Guid))
                {
                    BackendModel userBackendModel =  activeDirectorySearch.ToModel((DirectoryEntry)userInActiveDirectory.GetUnderlyingObject(), SUBJECT_TYPE.USER);
                    synchronizer.NotifyUserFound(userBackendModel);
                    updatedUserGuids.Add(userBackendModel.Guid);
                }
            }

            // einen Intersect zwischen den Benutzern im Spiegel-System und den bearbeiteten und existierenden Benutzern im System machen
            // die Schnittmenge sind die Benutzer, die im Active Directory nicht mehr gefunden werden konnten
            IEnumerable<Guid> nonExistingUserGuids = updatedUserGuids.Except(lookupElements);
            synchronizer.NotifyNonExistingUsers(nonExistingUserGuids);

            // Über die Zuweisungen der Benutzer zu Sicherheitsgruppen informieren
            foreach (Guid guidSicherheitsgruppe in allUserInGroups.Keys)
            {
                synchronizer.NotifyUsersInGroup(guidSicherheitsgruppe, allUserInGroups[guidSicherheitsgruppe]);
            }
        }
    }
}
