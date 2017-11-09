using CMDB01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMDB01.Controllers
{
    public class HomeController : Controller
    {
    
        public ActionResult Index()
        {
            CMDB db = new CMDB();

            ViewBag.accounts = db.accounts.Count();
            ViewBag.datasources = db.datasources.Count();
            ViewBag.contacts = db.contacts.Count();
            ViewBag.servers = db.serverFarms.Count();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}