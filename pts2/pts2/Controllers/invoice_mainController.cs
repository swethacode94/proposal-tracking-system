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
    public class invoice_mainController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: invoice_main
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult AutoComplete(string term)
        {
            var result = (from r in db.proposal join t in db.client_info on r.sent_to equals t.email_id
                          where r.prop_enquiry.ToLower().Contains(term.ToLower())
                          select new { r.prop_enquiry, r.sent_to, r.Id , t.firstname });

            



            return Json(result, JsonRequestBehavior.AllowGet);




        }

        [HttpPost]
        public JsonResult save(invoice_main O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dc = new ApplicationDbContext())
                {
                    invoice_main order = new invoice_main { pid= O.pid , proposal_name = O.proposal_name  };
                    foreach (var i in O.invoice_item)
                    {
                        i.imid = order.Id;
                        dc.invoice_item.Add(i);
                    }
                    dc.invoice_main.Add(order);
                    dc.SaveChanges();
                    status = true;
                }
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }

        // GET: invoice_main/Details/5
       
    }
}
