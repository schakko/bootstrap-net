using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecw.ActiveDirectory
{
    public class BackendModel
    {
        public BackendModel()
        {
        }

        public BackendModel(Guid guid, String displayName, BACKEND_TYPE backendType, long internalId, SUBJECT_TYPE subjectType, String distinguishedName)
        {
            Guid = guid;
            DisplayName = displayName;
            BackendType = backendType;
            InternalId = internalId;
            SubjectType = subjectType;
            DistinguishedName = distinguishedName;
        }

        public Guid Guid { get; set; }
        public String DisplayName { get; set; }
        public BACKEND_TYPE BackendType { get; set; }
        public long InternalId { get; set; }
        public SUBJECT_TYPE SubjectType { get; set; }
        public String DistinguishedName { get; set; }
        public Object NativeObject { get; set; }
    }
}
