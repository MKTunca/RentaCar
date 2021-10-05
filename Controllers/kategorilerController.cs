using RentAcar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAcar.Controllers
{
    public class kategorilerController : Controller
    {
        MVCBlogEntities baglan = new MVCBlogEntities();

        public ActionResult Index()
        {
            ViewBag.mesaj = "";
            var model = baglan.Akategoris.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string katAdi = fc["aKatTanim"];

            if ((katAdi != "") && (katAdi != null))
            {
                baglan.Akategoris.Add(new Akategori
                {
                    aKat = katAdi
                });

                baglan.SaveChanges();
            }
            else
            { ViewBag.mesaj = "Kategori adı yazınız."; }

            var model = baglan.Akategoris.ToList();
            return View(model);
        }

        public ActionResult kategoriB(int id)
        {
            ViewBag.mesaj = "";
            var model = baglan.Bkategoris.Where(m => m.aKat_id == id).ToList();

            ViewBag.akatID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult kategoriB(int id, FormCollection fc)
        {
            string katAdi = fc["bKatTanim"];

            if ((katAdi != "") && (katAdi != null))
            {
                baglan.Bkategoris.Add(new Bkategori
                {
                    bKat = katAdi,
                    aKat_id = id
                });

                baglan.SaveChanges();
            }
            else
            { ViewBag.mesaj = "Kategori adı yazınız."; }

            ViewBag.akatID = id;

            var model = baglan.Bkategoris.Where(m => m.aKat_id == id).ToList();
            return View(model);
        }

        public ActionResult kategoriC(int id)
        {
            ViewBag.mesaj = "";
            var model = baglan.Ckategoris.Where(m => m.bKat_id == id).ToList();

            ViewBag.bkatID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult kategoriC(int id, FormCollection fc)
        {
            string katAdi = fc["cKatTanim"];

            if ((katAdi != "") && (katAdi != null))
            {
                baglan.Ckategoris.Add(new Ckategori
                {
                    cKat = katAdi,
                    bKat_id = id
                });

                baglan.SaveChanges();
            }
            else
            { ViewBag.mesaj = "Kategori adı yazınız."; }

            ViewBag.bkatID = id;

            var model = baglan.Ckategoris.Where(m => m.bKat_id == id).ToList();
            return View(model);
        }
    }
}