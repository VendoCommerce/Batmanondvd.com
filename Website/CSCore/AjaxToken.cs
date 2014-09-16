using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSCore
{
    /// <summary>
    /// Use this class as a generic data container for sending/receiving asynch http data. 
    /// The meaning of each field is arbitrary and depends on the user.
    /// </summary>
    public class AjaxToken
    {
        public int versionId;
        public string operation;
        public string expireDateTime;
        public string userIP;
        public int random;
    }
}
