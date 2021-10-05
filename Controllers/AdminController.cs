using RentAcar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RentAcar.Controllers
{
    
    public class AdminController : Controller
    {
        MVCBlogEntities baglan = new MVCBlogEntities();

        public ActionResult login()
        {
            if(User.Identity.Name != "")
            {
                FormsAuthentication.SignOut();
            }
            return View();
        }

        [HttpPost]
        public ActionResult login(AdminTablo yt)
        {
            if (yt.Email != null)
            {
                if (yt.sifre != null)
                {
                    var bilgi = baglan.AdminTabloes.FirstOrDefault(m => m.Email == yt.Email && m.sifre == yt.sifre);

                    if (bilgi != null)
                    {
                        FormsAuthentication.RedirectFromLoginPage(bilgi.ID.ToString(), false);
                        return RedirectToAction("aracListe", "Araclar");
                    }
                    else
                    {
                        ViewBag.uyari = "Bilgilerinizi kontrol ediniz.";
                    }
                }
                else
                {
                    ViewBag.uyari = "Şifrenizi yazınız.";
                }
            }
            else
            {
                ViewBag.uyari = "E-mail adresinizi yazınız.";
            }
            return View();
        }
       
        public ActionResult adminListe()
        {
            var model = baglan.AdminTabloes.ToList();
            return View(model);
        }
        
        public ActionResult adminEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult adminEkle(AdminTablo adminYN)
        {
            if ((adminYN.Email != null) && (adminYN.sifre != null))
            {           
                baglan.AdminTabloes.Add(adminYN);
                baglan.SaveChanges();
                Session["uyari"] = "Bilgileriniz kaydedildi.";
            }
            else
            {
                Session["uyari"] = "Alanları doldurunuz.";
            }
            return RedirectToAction("adminListe");

        }

        [HttpPost]
        public JsonResult degisim(AdminTablo adminyonetim)
        {
            string cevap = "";
            if (User.Identity.Name != "")
            {
                var degisim = baglan.AdminTabloes.Find(adminyonetim.ID);
                degisim.Email = adminyonetim.Email;
                degisim.sifre = adminyonetim.sifre;        
                cevap = "Bilgileriniz Güncellendi.";
            }
            else
            { cevap = "Yetkisiz giriş"; }
            baglan.SaveChanges();
            return Json(cevap, JsonRequestBehavior.AllowGet);
        }
    }
}