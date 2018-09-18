using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using POC_WebApplication.Models;
using POC_WebApplication.ViewModel;

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

        public ConversionXml UpdateXmlFile(ConversionXmlModel xmlFile)
        {
            var node = xmlFile.XmlModel;

            // Build the document
            XDocument xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                    // This is the root of the document

                    new XElement(node[0].NodeName,

                    from emp in node
                    where emp.ParentId == 1
                    select
                        new XElement("Child",
                            from ch in node
                            where ch.ParentId == emp.NodeId
                            select new XElement("key", ch.NodeValue)
                            )
                            ));

      
            var selectedXml = _context.ConversionXmls.Where(x => x.Xml_Id == xmlFile.Xml_id).FirstOrDefault();
            selectedXml.Xml_TypeId = xmlFile.Xml_TypeId;
            selectedXml.XmlFile = xdoc.ToString();

            _context.SaveChanges();
            return selectedXml;

        }

        public void ConvertXmlintoModel()
        {

        }
    }
}