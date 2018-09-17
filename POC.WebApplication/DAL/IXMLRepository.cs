﻿
using POC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_WebApplication.DAL

   
{

  
    public interface IXMLRepository
    {
        IEnumerable<ConversionXml> GetAllXmlFile();
        ConversionXml GetXmlFileById(int XmlId);
    }
}