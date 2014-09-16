using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness.Attributes
{
    public interface IObjectAttribute
    {
        IEnumerable<string> GetAttributeNames();
        string GetAttributeValue(string attributeName);
    }
}
