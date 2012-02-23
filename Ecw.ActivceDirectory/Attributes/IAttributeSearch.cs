using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecw.ActiveDirectory.Attributes
{
    public interface IAttributeSearch
    {
        String CreateAttributeSearcher(String searchString);
    }
}
