using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecw.ActiveDirectory.Attributes
{
    public class StringAttributeSearch : AttributeSearchAdapter
    {
        public bool appendWildcards = false;
        
        public bool AppendWildcards
        {
            get
            {
                return appendWildcards;
            }
            set
            {
                appendWildcards = value;
            }
        }

        public StringAttributeSearch(String assignedAttribute)
            : base(assignedAttribute)
        {
        }

        public override string CreateAttributeSearcher(string searchString)
        {
            char[] invalidChars = new char[] { ')', '(', ';', ',', '*', ':', '&', '=' };

            // Alle gefährlichen Zeichen entfernen
            foreach (char c in invalidChars)
            {
                searchString = searchString.Replace(c.ToString(), "");
            }

            // Nach Leerzeichen splitten
            String[] arrSearch = searchString.Split(new char[] { ' ' });

            StringBuilder sb = new StringBuilder();

            foreach (String elem in arrSearch)
            {
                sb.Append("(")
                    .Append(AssignedAttribute)
                    .Append("=");
                if (!AppendWildcards)
                {
                    sb.Append(elem);
                }
                else
                {
                    sb.Append("*");
                    sb.Append(elem);
                    sb.Append("*");
                }

                sb.Append("*")
                    .Append(")");
            }

            return sb.ToString();
        }
    }
}
