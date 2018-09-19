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
            var nodelist = xmlFile.XmlModel.Where(s => s.NodeId != 1);
            
            XDocument xdoc = new XDocument(new XElement(node[0].NodeName,
                 from item in nodelist 
                 select new XElement(item.NodeName,item.NodeValue)));

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