using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace POC_WebApplication.Services
{
    public class XmlModel
    {
      
            public string NodeName { get; set; }
            public IEnumerable<XAttribute> Attributes { get; set; }
            public int NodeId { get; set; }
            public int stringId { get; set; }
            public int ParentId { get; set; }

            public string NodeValue { get; set; }
    }



    
}

