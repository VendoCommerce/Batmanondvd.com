using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness
{
    [Serializable]
    public class VersionCategory
    {

        public int CategoryId { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public bool HideRemove { get; set; }
  
    }
}
