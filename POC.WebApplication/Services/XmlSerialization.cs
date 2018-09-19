using POC_WebApplication.DAL;
using POC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace POC_WebApplication.Services
{
    
    public class XmlSerialization
    {
        
        public List<XmlModel> ProcessXml(int xmlId)
        {
           
            XMLRepository xr = new XMLRepository(new POCEntities());
            XmlDocument doc = new XmlDocument();
            var a = xr.GetXmlFileById(xmlId);
            doc.LoadXml(a.XmlFile);

            var xml =
                    XElement.Parse(a.XmlFile);

            var elementsAndIndex =
                xml
                .DescendantsAndSelf()
                .Select((node, index) => new { index = index + 1, node })
                .ToList();

            List<XmlModel> elementsWithIndexAndParentIndex =
                elementsAndIndex
                .Select(
                    elementAndIndex =>
                        new
                        {
                            Element = elementAndIndex.node,
                            Index = elementAndIndex.index,
                            ParentElement = elementsAndIndex.SingleOrDefault(parent => parent.node == elementAndIndex.node.Parent)
                        })
                .Select(
                    elementAndIndexAndParent =>
                        new XmlModel
                        {
                            NodeName = elementAndIndexAndParent.Element.Name.LocalName,
                            NodeId = elementAndIndexAndParent.Index,
                            ParentId = elementAndIndexAndParent.ParentElement == null ? 0 : elementAndIndexAndParent.ParentElement.index,
                            NodeValue=elementAndIndexAndParent.Element.HasElements == true? null : elementAndIndexAndParent.Element.Value,
                           
                        })
                .ToList();

            return elementsWithIndexAndParentIndex;
        }
   
     
    }
    
}