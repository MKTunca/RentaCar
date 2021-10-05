using RentAcar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAcar.Controllers
{
    public class SayfaController : Controller
    {
        MVCBlogEntities baglan = new MVCBlogEntities();

        public ActionResult Index()
        {
            var kategoriler = baglan.Akategoris.ToList();
            return View(kategoriler);
        }
    }
}