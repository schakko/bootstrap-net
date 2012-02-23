using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecw.ActiveDirectory.Attributes
{
    abstract public class AttributeSearchAdapter : IAttributeSearch
    {
        public String AssignedAttribute {get; set;}

        public AttributeSearchAdapter(String assignedAttribute)
        {
            AssignedAttribute = assignedAttribute;
        }

        abstract public String CreateAttributeSearcher(String searchString);

    }
}
