using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecw.ActiveDirectory
{
    public interface IBackendSearchProvider
    {
        IEnumerable<BackendModel> Search(String searchString, SUBJECT_TYPE subjectType);
    }
}
