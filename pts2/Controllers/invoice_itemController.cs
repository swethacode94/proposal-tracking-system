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
    public class invoice_itemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: invoice_item
        public ActionResult Index()
        {
            var invoice_item = db.invoice_item.Include(i => i.invoice_main);
            return View(invoice_item.ToList());
        }

        // GET: invoice_item/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice_item invoice_item = db.invoice_item.Find(id);
            if (invoice_item == null)
            {
                return HttpNotFound();
            }
            return View(invoice_item);
        }

        // GET: invoice_item/Create
        public ActionResult Create()
        {
            ViewBag.imid = new SelectList(db.invoice_main, "Id", "proposal_name");
            return View();
        }

        // POST: invoice_item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,imid,item,quantity,rate,total")] invoice_item invoice_item)
        {
            if (ModelState.IsValid)
            {
                db.invoice_item.Add(invoice_item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.imid = new SelectList(db.invoice_main, "Id", "proposal_name", invoice_item.imid);
            return View(invoice_item);
        }

        // GET: invoice_item/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice_item invoice_item = db.invoice_item.Find(id);
            if (invoice_item == null)
            {
                return HttpNotFound();
            }
            ViewBag.imid = new SelectList(db.invoice_main, "Id", "proposal_name", invoice_item.imid);
            return View(invoice_item);
        }

        // POST: invoice_item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,imid,item,quantity,rate,total")] invoice_item invoice_item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice_item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.imid = new SelectList(db.invoice_main, "Id", "proposal_name", invoice_item.imid);
            return View(invoice_item);
        }

        // GET: invoice_item/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice_item invoice_item = db.invoice_item.Find(id);
            if (invoice_item == null)
            {
                return HttpNotFound();
            }
            return View(invoice_item);
        }

        // POST: invoice_item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            invoice_item invoice_item = db.invoice_item.Find(id);
            db.invoice_item.Remove(invoice_item);
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
