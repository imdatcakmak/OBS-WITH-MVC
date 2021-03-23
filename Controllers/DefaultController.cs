using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var dersler = db.Tbl_Dersler.ToList();
            return View(dersler);
        }

        [HttpGet]
        public ActionResult YeniDers()
        {

            return View();
        }

        [HttpPost]
        public ActionResult YeniDers(Tbl_Dersler p)
        {
            db.Tbl_Dersler.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var ders = db.Tbl_Dersler.Find(id);
            db.Tbl_Dersler.Remove(ders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DersGetir(int id)
        {
            var ders = db.Tbl_Dersler.Find(id);
            return View("DersGetir",ders);
        }
        public ActionResult Guncelle(Tbl_Dersler p)
        {
            var ders = db.Tbl_Dersler.Find(p.DERSID);
            ders.DERSAD = p.DERSAD;
            db.SaveChanges();
            return RedirectToAction("Index", "Default");
        }
    }
}