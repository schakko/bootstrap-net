using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;


namespace Ecw.ActiveDirectory
{
    public class BackendSearchDelegator
    {
        private Dictionary<SUBJECT_TYPE, Dictionary<BACKEND_TYPE, List<IBackendSearchProvider>>> backendSearchers = new Dictionary<SUBJECT_TYPE, Dictionary<BACKEND_TYPE, List<IBackendSearchProvider>>>();

        /// <summary>
        /// registers a new backend search provider for given subject with given type
        /// </summary>
        /// <param name="subjectType"></param>
        /// <param name="backendType"></param>
        /// <param name="provider"></param>
        public void RegisterProvider(SUBJECT_TYPE subjectType, BACKEND_TYPE backendType, IBackendSearchProvider provider)
        {
            if (!backendSearchers.ContainsKey(subjectType)) {
                backendSearchers.Add(subjectType, new Dictionary<BACKEND_TYPE, List<IBackendSearchProvider>>());
            }

            if (!backendSearchers[subjectType].ContainsKey(backendType))
            {
                backendSearchers[subjectType].Add(backendType, new List<IBackendSearchProvider>());
            }

            backendSearchers[subjectType][backendType].Add(provider);
        }

        /// <summary>
        /// executes the search over all given search providers
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="providers"></param>
        /// <param name="subjectType"></param>
        /// <returns></returns>
        public IEnumerable<BackendModel> PerformSearch(String searchString, IEnumerable<IBackendSearchProvider> providers, SUBJECT_TYPE subjectType)
        {
            List<BackendModel> r = new List<BackendModel>();

            foreach (IBackendSearchProvider provider in providers)
            {
                r.AddRange(provider.Search(searchString, subjectType));
            }

            return r;
        }

        /// <summary>
        /// Finds all subjects in a given backend
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="backendType"></param>
        /// <returns></returns>
        public IEnumerable<BackendModel> Search(String searchString, BACKEND_TYPE backendType)
        {
            List<BackendModel> r = new List<BackendModel>();

            foreach (KeyValuePair<SUBJECT_TYPE, Dictionary<BACKEND_TYPE, List<IBackendSearchProvider>>> kvp in backendSearchers)
            {
                List<IBackendSearchProvider> backendsForSubject = kvp.Value[backendType];

                if (backendsForSubject == null)
                {
                    continue;
                }

                foreach (IBackendSearchProvider providerInBackend in backendsForSubject)
                {
                    r.AddRange(providerInBackend.Search(searchString, kvp.Key));
                }
            }

            return r;
        }

        /// <summary>
        /// does a search over all backends for the given subject
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="subjectType"></param>
        /// <returns></returns>
        public IEnumerable<BackendModel> Search(String searchString, SUBJECT_TYPE subjectType)
        {
            if (!backendSearchers.ContainsKey(subjectType))
            {
                return new List<BackendModel>();
            }

            List<IBackendSearchProvider> designatedProviders = new List<IBackendSearchProvider>();

            foreach (KeyValuePair<BACKEND_TYPE, List<IBackendSearchProvider>> providerByBackend in backendSearchers[subjectType])
            {
                designatedProviders.AddRange(providerByBackend.Value);
            }

            return PerformSearch(searchString, designatedProviders, subjectType);

        }

        /// <summary>
        /// does a search for the given subject over the given backend
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="subjectType"></param>
        /// <param name="backendType"></param>
        /// <returns></returns>
        public IEnumerable<BackendModel> Search(String searchString, SUBJECT_TYPE subjectType, BACKEND_TYPE backendType)
        {
            if (!backendSearchers.ContainsKey(subjectType) || !backendSearchers[subjectType].ContainsKey(backendType))
            {
                return new List<BackendModel>();
            }

            return PerformSearch(searchString, backendSearchers[subjectType][backendType], subjectType);
        }
    }
}
