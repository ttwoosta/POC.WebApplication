using POC_WebApplication.DAL;
using POC_WebApplication.Models;
using POC_WebApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_WebApplication.Controllers {
    public class HomeController : Controller {

        private IXMLRepository xmlRepository;

        public HomeController()
        {
            this.xmlRepository = new XMLRepository(new POCEntities());
        }

     
        public ActionResult Index() {
            return View();
        }
        
        public JsonResult GetAllXmlFile()
        {
            return Json(xmlRepository.GetAllXmlFile(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditXmlFileForm(int id)
        {
            
            var xl = xmlRepository.GetXmlFileById(id);
            ConversionXmlModel model = new ConversionXmlModel() {
                Xml_id=xl.Xml_Id,
                Xml_TypeId=xl.Xml_TypeId,
                XmlFile=xl.XmlFile
            };
            return PartialView("_EditXmlFileForm", model);
        }

        public JsonResult XmlFileTree()
        {
            List<XMLModel> xn = new List<XMLModel>();

            XMLModel xnode = new XMLModel();
            xnode.NodeName = "parent";
            xnode.NodeId = 1;
            xnode.ParentId = 0;
            xn.Add(xnode);

            for (int i = 2; i < 4; i++)
            {
                XMLModel xnode2 = new XMLModel();
                xnode2.NodeName = "Child";
                xnode2.NodeId = i;
                xnode2.ParentId = 1;
                xn.Add(xnode2);
            }

            for (int i = 4; i < 8; i++)
            {
                XMLModel xnode2 = new XMLModel();
                xnode2.NodeName = "Key";
                xnode2.NodeValue = "Value" + i;
                xnode2.NodeId = i;
                xnode2.ParentId = 2;
                xn.Add(xnode2);
            }
            return Json(xn, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateXMLFile(ConversionXmlModel xmlfile)
        {
            xmlRepository.UpdateXmlFile(xmlfile);
            return Json("success");
           
        }
    }
}