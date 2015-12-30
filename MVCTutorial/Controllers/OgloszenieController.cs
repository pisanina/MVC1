using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace MVCTutorial.Controllers
{
    public class OgloszenieController : Controller
    {
        private Entities1 db = new Entities1();
        private int _recordsOnPage = 3;

        //
        // GET: /Detail/

        public ActionResult Index()
        {
            var ogloszenie = db.Ogloszenie.Include(o => o.UserProfile).OrderBy(o=>o.Id).Take(_recordsOnPage);
            int pageCount = db.Ogloszenie.Count() / _recordsOnPage;
            if (db.Ogloszenie.Count() % _recordsOnPage > 0)
                ++pageCount;
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = 0;
            return View(ogloszenie.ToList());
        }

        public ActionResult GetPageRecords(int pageNumber)
        {
            var ogloszenie = db.Ogloszenie.Include(o => o.UserProfile).OrderBy(o => o.Id).Skip(pageNumber * _recordsOnPage).Take(_recordsOnPage);
            int pageCount = db.Ogloszenie.Count() / _recordsOnPage;
            if (db.Ogloszenie.Count() % _recordsOnPage > 0)
                ++pageCount;
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            return View("index",ogloszenie.ToList());
        }

        

        public ActionResult Szukaj()
        {
            //ViewBag.SprzedajacyID = new SelectList(db.Sprzedajacy, "Id", "Login");
            IList<UserProfile> listaSprzedajacych = new List<UserProfile>();
            listaSprzedajacych.Add(new UserProfile());
            listaSprzedajacych = listaSprzedajacych.Concat(db.UserProfile.ToList()).ToList();  

            

            ViewBag.ListaSprzedajacych = new SelectList(listaSprzedajacych, "UserId", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Szukaj(Models.OgloszenieSzukajViewModel parametryWyszukania)
        {
            var ogloszenia = db.Ogloszenie.Include(o => o.UserProfile);

            ogloszenia=ogloszenia.Where(o =>
                (String.IsNullOrEmpty(parametryWyszukania.Tytul) || o.Tytul.Contains(parametryWyszukania.Tytul))
                && (String.IsNullOrEmpty(parametryWyszukania.Opis) || o.Opis.Contains(parametryWyszukania.Opis))
                && (parametryWyszukania.CenaOd==0 || o.Cena>=parametryWyszukania.CenaOd)
                && (parametryWyszukania.CenaDo==0 || o.Cena<=parametryWyszukania.CenaDo)
                && (parametryWyszukania.SprzedajacyID == 0 || o.SprzedajacyID == parametryWyszukania.SprzedajacyID)
            );



                //ogloszenia = ogloszenia.Where(o => if(!String.IsNullOrEmpty(parametryWyszukania.Tytul)) o.Tytul.Contains(parametryWyszukania.Tytul));

            return View("Index", ogloszenia.ToList());






            
        }

        //
        // GET: /Detail/Details/5

        public ActionResult Details(int id = 0)
        {
            Ogloszenie ogloszenie = db.Ogloszenie.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }

        //
        // GET: /Detail/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.SprzedajacyID = new SelectList(db.UserProfile, "Id", "Login");
            return View();
        }

        //
        // POST: /Detail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                ogloszenie.SprzedajacyID =WebSecurity.GetUserId(User.Identity.Name);
                db.Ogloszenie.Add(ogloszenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SprzedajacyID = new SelectList(db.UserProfile, "Id", "Login", ogloszenie.SprzedajacyID);
            return View(ogloszenie);
        }


        public ActionResult SortujUp()
        {
            var ogloszenie = db.Ogloszenie.Include(o => o.UserProfile).OrderBy(o => o.Cena);

            return View("Index",ogloszenie);
        }

        public ActionResult SortujDown()
        {
            var ogloszenie = db.Ogloszenie.Include(o => o.UserProfile).OrderByDescending(o => o.Cena);

            return View("Index", ogloszenie);
        }

        //
        // GET: /Detail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ogloszenie ogloszenie = db.Ogloszenie.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.SprzedajacyID = new SelectList(db.UserProfile, "Id", "Login", ogloszenie.SprzedajacyID);
            return View(ogloszenie);
        }

        //
        // POST: /Detail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogloszenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SprzedajacyID = new SelectList(db.UserProfile, "Id", "Login", ogloszenie.SprzedajacyID);
            return View(ogloszenie);
        }

        //
        // GET: /Detail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ogloszenie ogloszenie = db.Ogloszenie.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }

        //
        // POST: /Detail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ogloszenie ogloszenie = db.Ogloszenie.Find(id);
            db.Ogloszenie.Remove(ogloszenie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}