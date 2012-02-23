using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecw.ActiveDirectory.Attributes
{
    public class GuidAttributeSearch : AttributeSearchAdapter
    {
        public GuidAttributeSearch(String assignedAttribute)
            : base(assignedAttribute)
        {
        }


        public override string CreateAttributeSearcher(string searchString)
        {
            String[] arrSearch = searchString.Split(new char[] { ' ' });
            StringBuilder sb = new StringBuilder();
            StringBuilder sbInner = new StringBuilder();
            System.Guid guid;

            // OR every attribute, cause there can't be two of them

            foreach (String search in arrSearch)
            {
                try
                {
                    guid = new Guid(searchString);
                }
                catch (Exception e)
                {
                    continue;
                }

                sbInner.Append("(");
                sbInner.Append(AssignedAttribute);
                sbInner.Append("=");
                byte[] byteGuid = guid.ToByteArray();
                string queryGuid = "";

                foreach (byte b in byteGuid)
                {
                    queryGuid += @"\" + b.ToString("x2");
                }

                sbInner.Append(queryGuid);
                sbInner.Append(")");
            }

            if (sbInner.Length == 0)
            {
                return null;
            }

            sb.Append("(|");
            sb.Append(sbInner.ToString());
            sb.Append(")");

            return sb.ToString();
        }
    }
}
