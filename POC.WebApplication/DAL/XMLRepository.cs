using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POC_WebApplication.Models;

namespace POC_WebApplication.DAL
{
    public class XMLRepository : IXMLRepository
    {

        private POCEntities _context;

        public XMLRepository(POCEntities context)
        {
            this._context = context;
        }
        public IEnumerable<ConversionXml> GetAllXmlFile()
        {
            return _context.ConversionXmls;
        }

        public ConversionXml GetXmlFileById(int XmlId)
        {
           return _context.ConversionXmls.Where(x => x.Xml_Id == XmlId).FirstOrDefault();
        }
    }
}