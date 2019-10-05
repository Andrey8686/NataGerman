using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POCO.DbModels;

namespace WebSite.Controllers
{
    public class BaseController : Controller
    {
        protected readonly DataContext db = new DataContext();
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}