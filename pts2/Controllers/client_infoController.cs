using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pts2.Models;

namespace pts2.Controllers
{
    public class client_infoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: client_info
        public ActionResult Index()
        {
            return View(db.client_info.ToList());
        }

        // GET: client_info/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client_info client_info = db.client_info.Find(id);
            if (client_info == null)
            {
                return HttpNotFound();
            }
            return View(client_info);
        }

    /*    public JsonResult isuserexists(string email_id)
        {
            return Json(!db.client_info.Any(c => c.email_id == email_id), JsonRequestBehavior.AllowGet);
        } */

      /*  public JsonResult isnoexists(string contact1)
        {
            return Json(!db.client_info.Any(c => c.contact1 == contact1), JsonRequestBehavior.AllowGet);
        } */

        public JsonResult GetCountries()
        {
            return Json(db.country.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIndustries()
        {
            return Json(db.industry.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: client_info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: client_info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(client_info client_info)
        {
            var userexist = db.client_info.Where(m => m.email_id == client_info.email_id).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (userexist == null)
                {
                    db.client_info.Add(client_info);
                    db.SaveChanges();
                    return RedirectToAction("Index", "client_info");
                }
                else
                {
                    ModelState.AddModelError("","Email already exists");
                    
                    return View(client_info);
                }
            }

            return View(client_info);
        }

        // GET: client_info/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client_info client_info = db.client_info.Find(id);
            if (client_info == null)
            {
                return HttpNotFound();
            }
            return View(client_info);
        }

        // POST: client_info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,companyname,address1,address2,contact1,contact2,email_id,vat_no")] client_info client_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client_info);
        }

        // GET: client_info/Delete/5
        
    /*  public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client_info client_info = db.client_info.Find(id);
            if (client_info == null)
            {
                return HttpNotFound();
            }
            return View(client_info);
        }*/

        // POST: client_info/Delete/5
       /* [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]*/
        public ActionResult DeleteConfirmed(long id)
        {
            client_info client_info = db.client_info.Find(id);
            db.client_info.Remove(client_info);
            db.SaveChanges();
            return RedirectToAction("Index");
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
