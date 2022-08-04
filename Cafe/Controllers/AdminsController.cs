using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cafe.Models;

namespace Cafe.Controllers
{
    public class AdminsController : Controller
    {
        private cofeDBEntities2 db = new cofeDBEntities2();

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["userId"] != null)
            {
                return RedirectToAction("Index", "Drinks");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "id,name,password")] Admin admin)
        {
            var rec = db.Admins.Where(x => x.name == admin.name && x.password == admin.password).ToList().FirstOrDefault();
            if (rec != null)
            {
                Session["username"] = rec.name;
                Session["userId"] = rec.id;
                return RedirectToAction("Index","Drinks");
            }
            else
            {
                ViewBag.errm = "Email or password is incorrect...";
            }
            return View(admin);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Admins");
        }
        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.OrderByDescending(x => x.id).ToList());
        }
     
        //// GET: Admins/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Admin admin = db.Admins.Find(id);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(admin);
        //}

        //  GET: Admins/Create
        public ActionResult Create()
        {
            
            return View();
        }

        //// POST: Admins/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Login", "Admins");
            }

            return View(admin);
        }

       // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

       // POST: Admins/Edit/5
       //  To protect from overposting attacks, enable the specific properties you want to bind to, for 
       //  more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

       // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Login", "Admins");
        }

        [HttpGet]
        public ActionResult Location()
        {
                return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
