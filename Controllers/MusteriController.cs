using RentAcar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAcar.Controllers
{
    [_AdminControl]
    public class MusteriController : Controller
    {
        MVCBlogEntities baglan = new MVCBlogEntities();
       
        public ActionResult musteriListe()
        {
            var model = baglan.MusteriTabloes.ToList();

            List<object> drm = new List<object>();

            drm.Add(new SelectListItem { Text = "Aktif", Value = "Aktif" });
            drm.Add(new SelectListItem { Text = "Pasif", Value = "Pasif" });

            ViewBag.durumlar = drm;

            return View(model);
        }

        public JsonResult musteriSil(int id)
        {
            var silinecekData = baglan.MusteriTabloes.Find(id);

            baglan.MusteriTabloes.Remove(silinecekData);

            baglan.SaveChanges();

            return Json("Silme işlemi tamamlandı.", JsonRequestBehavior.AllowGet);
        }
        public ActionResult musteriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult  musteriEkle(MusteriTablo MusteriYonetim)
        {
            if ((MusteriYonetim.AdSoyad != null) && (MusteriYonetim.Email != null) &&(MusteriYonetim.sifre != null) &&(MusteriYonetim.Telefon != null))
            {
                MusteriYonetim.durum = "Aktif";
                baglan.MusteriTabloes.Add(MusteriYonetim);
                baglan.SaveChanges();
                Session["uyari"] = "Bilgileriniz kaydedildi.";
            }
            else
            {
                Session["uyari"] = "Gerekli alanları doldurunuz ";
            }
            return RedirectToAction("musteriListe");
        }
        [HttpPost]
        public JsonResult musteriDegisim(MusteriTablo musteriyonetim)
        {
            var cevap = "";
            var degisim = baglan.MusteriTabloes.Find(musteriyonetim.ID);

          if (User.Identity.Name != "") { 

             degisim.AdSoyad = musteriyonetim.AdSoyad;
             degisim.Dogumyil = musteriyonetim.Dogumyil;
             degisim.EhliyetTipi = musteriyonetim.EhliyetTipi;
             degisim.Email = musteriyonetim.Email;
             degisim.il = musteriyonetim.il;
             degisim.ilce = musteriyonetim.ilce;
             cevap = "Bilgiler güncellendi";
                baglan.SaveChanges();
          }
          else
          {
              cevap = "Yetkisiz Giriş";
          }
          
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
    }
}