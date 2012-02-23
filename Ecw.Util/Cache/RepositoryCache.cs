using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecw.Util.Cache
{
    /// <summary>
    /// Simpler Cache für Repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryCache<T>
    {
        /// <summary>
        /// Wird aufgerufen, sobald der Cache neu geladen werden muss
        /// </summary>
        /// <returns></returns>
        public delegate IEnumerable<T> RefreshHandler();

        /// <summary>
        /// Legt fest, dass der Cache verschmutzt ist, d.h. beim nächsten Zugriff neu geladen werden muss
        /// </summary>
        public bool IsDirty = true;

        /// <summary>
        /// privater Cache
        /// </summary>
        private IEnumerable<T> cache = new List<T>();

        /// <summary>
        /// Handler, der aufgerufen wird
        /// </summary>
        public RefreshHandler Refresh;

        public IEnumerable<T> Cache
        {
            get
            {
                if (IsDirty)
                {
                    lock (lockObject)
                    {
                        if (Refresh != null)
                        {
                            cache = Refresh();
                        }

                        IsDirty = false;
                    }
                }

                return cache;
            }
            set
            {
                cache = value;
            }
        }

        /// <summary>
        /// Mutext
        /// </summary>
        private object lockObject = new object();

        /// <summary>
        /// Benachrichtgt den Cache über ein Ereignis, dass der Cache neu geladen werden muss
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void NotifyDirty(object sender, EventArgs args)
        {
            IsDirty = true;
        }
    }
}
