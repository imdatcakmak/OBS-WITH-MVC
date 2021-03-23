using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();

        // GET: Ogrenci
        public ActionResult Index()
        {
            var ogrenciler = db.Tbl_Ogrenciler.ToList();
            return View(ogrenciler);
        }
        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kulupler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }
                                           ).ToList();

            ViewBag.dgr = degerler;

            return View();
        }
        [HttpPost]
        public ActionResult YeniOgrenci(Tbl_Ogrenciler p)
        {
            var klp = db.Tbl_Kulupler.Where(m => m.KULUPID == p.Tbl_Kulupler.KULUPID).FirstOrDefault();
            p.Tbl_Kulupler = klp;
            db.Tbl_Ogrenciler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var ogr = db.Tbl_Ogrenciler.Find(id);
            db.Tbl_Ogrenciler.Remove(ogr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OgrenciGetir(int id)
        {
            var ogr = db.Tbl_Ogrenciler.Find(id);

            List<SelectListItem> degerler = (from i in db.Tbl_Kulupler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }
                                          ).ToList();

            ViewBag.dgr = degerler;

            return View("OgrenciGetir", ogr);
        }
        public ActionResult Guncelle(Tbl_Ogrenciler p)
        {
            var ogr = db.Tbl_Ogrenciler.Find(p.OGRENCIID);
            ogr.OGRAD = p.OGRAD;
            ogr.OGRSOYAD = p.OGRSOYAD;
            ogr.OGRFOTO = p.OGRFOTO;
            ogr.OGRCINSIYET = p.OGRCINSIYET;
            //  ogr.OGRKULUP = p.OGRKULUP;
            var kulup = db.Tbl_Kulupler.Where(m => m.KULUPID == p.Tbl_Kulupler.KULUPID).FirstOrDefault();
            ogr.OGRKULUP = kulup.KULUPID;
            db.SaveChanges();
            return RedirectToAction("Index", "Ogrenci");
        }
    }
}