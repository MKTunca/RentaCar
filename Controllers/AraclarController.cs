using RentAcar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAcar.Controllers
{
    [_AdminControl]
    public class AraclarController : Controller
    {
        MVCBlogEntities baglan = new MVCBlogEntities();
   
        public ActionResult aracListe()
        {
            try
            {
                ViewBag.uyarTXT = Session["uyari"];
            }
            catch
            { Session["uyari"] = ""; }
            var model = baglan.AracTabloes.ToList();

            List<object> drm = new List<object>();

            drm.Add(new SelectListItem { Text = "Aktif", Value = "Aktif" });
            drm.Add(new SelectListItem { Text = "Pasif", Value = "Pasif" });
            ViewBag.durumlar = drm;
            return View(model);
        }

        public ActionResult aracEkle()
        {
            var Aliste = baglan.Akategoris.ToList();
            ViewBag.AnaKategori = Aliste;

            ViewBag.maxYil = DateTime.Now.Year;

            Int32 akatID = Aliste[0].ID;
            ViewBag.AltKategori = baglan.Bkategoris.Where(m => m.aKat_id == akatID).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult aracEkle(AracTablo Aracyonetici, HttpPostedFileBase dosya)
        {
            var cevap = "";
            if (User.Identity.Name != "") { 
          
                if ((Aracyonetici.Marka != null) && (Aracyonetici.Model != null))
                {
                Aracyonetici.imaj = "";

                    if (dosya != null)
                    {
                        string sonuc = dosyaIslem(dosya);
                        if (sonuc.IndexOf("Hata : ") == -1)
                        {
                            Aracyonetici.imaj = "~/imajlar/" + sonuc;
                        }
                    }
                    Aracyonetici.durum = "Aktif";
                    baglan.AracTabloes.Add(Aracyonetici);
                    baglan.SaveChanges();
                    Session["uyari"] = "Bilgileriniz kaydedildi.";  
                }
                else
                {
                    Session["uyari"] = "E-mail ve şifre alanları zorunlu.";
                }

            }
            else
            {
                Session["uyari"] = "Yetkisiz giriş";
            }
            return RedirectToAction("aracListe");
        }

        public JsonResult aracSil(int id)
        {
            var cevap = "";
            var silinecekData = baglan.AracTabloes.Find(id);

            if (User.Identity.Name != "") { 

               baglan.AracTabloes.Remove(silinecekData);

               cevap = "Bilgiler Silindi";
               baglan.SaveChanges();
            }
            else
            {
                cevap = "Yetkisiz giriş";
            }
            return Json(cevap, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult aracDegistir(AracTablo aracyonetim)
        //{
        //    string cevap = "";

        //    //if (User.Identity.Name != "")
        //    //{
        //        var degisim = baglan.AracTabloes.Find(aracyonetim.ID);
        //        degisim.Marka = aracyonetim.Marka;
        //        degisim.Model = aracyonetim.Model;
        //        degisim.Plaka= aracyonetim.Plaka;
        //        degisim.Vites_tipi = aracyonetim.Vites_tipi;
        //        degisim.yakit_tipi = aracyonetim.yakit_tipi;
          
        //        degisim.yil = aracyonetim.yil;

        //        if (aracyonetim.durum != null)
        //        { degisim.durum = aracyonetim.durum; }

        //        baglan.SaveChanges();
        //        cevap = "Bilgileriniz Güncellendi.";
        //    //}
        //    //else
        //    //{ cevap = "Yetkisiz giriş"; }

        //    return Json(cevap, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult aracDegis(AracTablo aracyonet)
        {
            var cevap = "";
            var degisim = baglan.AracTabloes.Find(aracyonet.ID);
           
            if (User.Identity.Name != "")
            {     
              degisim.Marka = aracyonet.Marka;
              degisim.Model = aracyonet.Model;
              degisim.Plaka = aracyonet.Plaka;
              degisim.Vites = aracyonet.Vites;
              degisim.yakit = aracyonet.yakit;
              degisim.yil = aracyonet.yil;
              degisim.durum = aracyonet.durum;

                cevap = "Bilgileriniz Güncellendi";
            }
            else
            {
                cevap = "Yetkisiz giriş";
            }
            baglan.SaveChanges();
            return Json(cevap, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult imaj_degistir(int id)
        {
            var cevap = "";
            var degisim = baglan.AracTabloes.Find(id);
           string sonuc = dosyaIslem(Request.Files[0]);
           if (User.Identity.Name != "")
           { 
             if (sonuc.IndexOf("Hata : ") == -1)
             {
                degisim.imaj = "~/imajlar/" + sonuc;
                    cevap = "Fotoğraf Güncellendi";
                baglan.SaveChanges();
             }
           }
            else
            {
                cevap = "Yetkisiz giriş";
            }

            return Json(cevap, JsonRequestBehavior.AllowGet);
        }
        public string dosyaIslem(HttpPostedFileBase dosya)
        {
            string sonuc = "";
            int dosyaMB = dosya.ContentLength;
            string dosyaTanim = dosya.FileName;
            string mimeTip = dosya.ContentType;
            string uzanti = dosyaTanim.Substring(dosyaTanim.LastIndexOf("."));

            if (dosya.ContentLength < 1000000)
            {
                if (uzanti.IndexOf("js") == -1)
                {
                    dosya.SaveAs(Server.MapPath("~/imajlar/") + dosyaTanim);
                    sonuc = dosyaTanim;
                }
                else
                {
                    sonuc = "Hata : Dosya geçersiz.";
                }
            }
            else
            {
                sonuc = "Hata : Dosya 1 mb daha büyük.";
            }
            return sonuc;
        }

        public JsonResult altkategoriler(int id)
        {
            var altListe = baglan.Bkategoris.Where(m => m.aKat_id == id).ToList();

            return Json(altListe, JsonRequestBehavior.AllowGet);
        }
    }
}