using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Ecw.ActiveDirectory
{
    public interface IBackendSynchronizer
    {
        /// <summary>
        /// Liefert die GUIDs der Gruppen zurück, die im Spiegel-System hinterlegt sind
        /// </summary>
        /// <returns></returns>
        IEnumerable<Guid> GetRegisteredGroups();
        
        /// <summary>
        /// Liefert alle GUIDs von Benutzern zurück, die im Spiegel-System hinterlegt sind.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Guid> GetRegisteredUsers();
        
        /// <summary>
        /// Informiert über die Gruppen, die nicht mehr im Active Directory vorhanden sind
        /// </summary>
        /// <param name="guid"></param>
        void NotifyNonExistingGroups(IEnumerable<Guid> groups);
        
        /// <summary>
        /// Informiert über die Benutzer, der nicht mehr im Active Directory hinterlegt ist
        /// </summary>
        /// <param name="users"></param>
        void NotifyNonExistingUsers(IEnumerable<Guid> users);

        /// <summary>
        /// Informiert darüber, dass eine Gruppe gefunden worden ist
        /// </summary>
        /// <param name="group"></param>
        void NotifyGroupFound(BackendModel group);

        /// <summary>
        /// Informiert darüber, dass ein Benutzer gefunden wurde.
        /// Es wird über jeden Benutzer jeweils nur genau einmal informiert und nicht mehrmals!
        /// </summary>
        /// <param name="user"></param>
        void NotifyUserFound(BackendModel user);

        /// <summary>
        /// Informiert darüber, dass diese Benutzer in der Gruppe vorhanden sind.
        /// Diese Methode wird aufgerufen, *nachdem* der BackendSynchronizer über alle Benutzer informiert worden ist
        /// </summary>
        /// <param name="group"></param>
        /// <param name="users"></param>
        void NotifyUsersInGroup(Guid group, IEnumerable<Guid> users);
    }
}
