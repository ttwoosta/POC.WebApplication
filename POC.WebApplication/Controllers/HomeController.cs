using Newtonsoft.Json;
using POC_WebApplication.DAL;
using POC_WebApplication.Models;
using POC_WebApplication.Services;
using POC_WebApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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

        public JsonResult XmlFileTree(int xmlId)
        {
            XmlSerialization xs = new XmlSerialization();
           var model =  xs.ProcessXml(xmlId);
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateXMLFile(ConversionXmlModel xmlfile)
        {
            xmlRepository.UpdateXmlFile(xmlfile);
            return Json("success");
           
        }


       
    }
}