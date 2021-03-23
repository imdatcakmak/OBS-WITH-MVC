using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using OgrenciNotMvc.Models;
namespace OgrenciNotMvc.Controllers
{
    public class NotlarController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        // GET: Notlar
        public ActionResult Index()
        {
            var notlar = db.Tbl_Notlar.ToList();
            return View(notlar);
        }
        [HttpGet]
        public ActionResult YeniSinav()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSinav(Tbl_Notlar p)
        {
            db.Tbl_Notlar.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult NotGetir(int id)
        {
            var not = db.Tbl_Notlar.Find(id);
            return View("NotGetir", not);
        }
        [HttpPost]
        public ActionResult NotGetir(Class1 model,Tbl_Notlar p,int SINAV1=0,int SINAV2=0,int SINAV3=0,int PROJE=0,int DURUM=0)
        {
            var snv = db.Tbl_Notlar.Find(p.NOTID);
            if (model.islem == "HESAPLA")
            {
                //işlem1
                int ORTALAMA = (SINAV1 + SINAV2 + SINAV3 + PROJE) / 4;
                ViewBag.ort = ORTALAMA;
                if (p.ORTALAMA >= 50)
                {
                    snv.DURUM = true;
                }
                else
                {
                    snv.DURUM = false;
                }
            }
            if (model.islem == "NOTGUNCELLE")
            {
               
                snv.SINAV1 = p.SINAV1;
                snv.SINAV2 = p.SINAV2;
                snv.SINAV3 = p.SINAV3;
                snv.ORTALAMA = p.ORTALAMA;
                snv.PROJE = p.PROJE;
               
                db.SaveChanges();
                return RedirectToAction("Index", "Notlar");
            }
            return View();
           
        }
    }
}