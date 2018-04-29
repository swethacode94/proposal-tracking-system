using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pts2.Models;
using System.IO;
using System.IO.Compression;


using ICSharpCode;
using ICSharpCode.SharpZipLib.Zip;



namespace pts2.Controllers
{
    public class enquiry_infoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: enquiry_info
        public ActionResult Index()
        {
            var enquiry = db.enquiry.Include(e => e.client_info);
            return View(enquiry.ToList());
        }

        // GET: enquiry_info/Details/5
        public ActionResult Details(long? id,string search)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            enquiry_info enquiry_info = db.enquiry.Find(id);
          
            if (enquiry_info == null)
            {
                return HttpNotFound();
            }
          
            
            return View(enquiry_info);
        }
        

        [HttpPost]
        public ActionResult AutoComplete(string term)
        {
           
            var result = (from r in db.client_info
                          where r.email_id.ToLower().Contains(term.ToLower())
                          select new { r.email_id,r.firstname});


                return Json(result, JsonRequestBehavior.AllowGet);
           

           
           

        }

        
        public ActionResult Save()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Save(client_info client_info)
        {
          
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dc = new ApplicationDbContext())
                {
                    var userexist = dc.client_info.Where(m => m.email_id == client_info.email_id).SingleOrDefault();
                    if (userexist == null) {
                        //Save
                        dc.client_info.Add(client_info);

                        dc.SaveChanges();
                        status = true; }
                    else
                    {
                        status = false;
                    }
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

       
        // GET: enquiry_info/Create
        public ActionResult Create()
        {
            ViewBag.cid = new SelectList(db.client_info, "id", "firstname");
            return View();
        }

        // POST: enquiry_info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(enquiry_info enquiry_info, string search)
        {
            
            var userexist = db.client_info.Where(m => m.email_id == search).SingleOrDefault();

            if (ModelState.IsValid)
            {
                if (userexist != null)
                {

                    List<attachments_info> attachment = new List<attachments_info>();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                            attachments_info attachment_info = new attachments_info()
                            {
                                fileName = fileName


                            };

                            attachment.Add(attachment_info);
                            var extension = Path.GetExtension(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), attachment_info.fileName);
                            file.SaveAs(path);

                        }
                    }

                    var q = from c in db.client_info where c.email_id == search select c;
                    foreach (var cust in q)
                    {
                        enquiry_info.cid = cust.id;
                    }

                    db.enquiry.Add(enquiry_info);
                    enquiry_info.attachments = attachment;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "Email does not exists");

                    return View(enquiry_info);

                }
                }

            

            ViewBag.cid = new SelectList(db.client_info, "id", "firstname", enquiry_info.cid);

            return View(enquiry_info);
        }

        
       public void Download(long id)
        {
      /* attachments_info attachment;
            
          attachment = db.attachment.FirstOrDefault(x => x.cid == id);
 
         
              return File("~/App_Data/Upload/" + attachment.fileName, "application/force-download", Path.GetFileName(attachment.fileName));
            
           */

            string zipfilename = Guid.NewGuid() + "MyZip.zip";
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition",zipfilename);
          byte[] buffer = new byte[1000000];
            ZipOutputStream zipoutputstream = new ZipOutputStream(Response.OutputStream);
            zipoutputstream.SetLevel(3);
            try
            {

                // attachments_info attachment;

                //  attachment = db.attachment.FirstOrDefault(x => x.cid == id);

                var q = from c in db.attachment where c.cid == id select c;




                DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/App_Data/Upload/"));


                foreach (var i in di.GetFiles())
                {
                    if (q.Count(c => c.fileName == i.Name) > 0)
                    {

                        Stream fs = System.IO.File.OpenRead(i.FullName);
                       
                        ZipEntry zipentry = new ZipEntry(ZipEntry.CleanName(i.Name));
                        zipentry.Size = fs.Length;
                        zipoutputstream.PutNextEntry(zipentry);
                      
                        int count = fs.Read(buffer, 0, buffer.Length);
                        while (count > 0)
                        {
                           
                          //  BinaryReader br = new BinaryReader(fs);
                          //  Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            zipoutputstream.Write(buffer, 0, count);
                            count = fs.Read(buffer, 0, buffer.Length);
                            if (!Response.IsClientConnected)
                            {
                                break;
                            }
                          //  Response.BinaryWrite(bytes);
                            Response.Flush();
                        }
                        fs.Close();
                    }
                }

                    zipoutputstream.Close();
                    Response.Flush();
                    Response.End();

                
            }

            catch (Exception)
            {
                throw;
            }

          
         
        }
       

        
       
        // GET: enquiry_info/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // enquiry_info enquiry_info = db.enquiry.Find(id);
            enquiry_info enquiry_info = db.enquiry.Include(s => s.attachments).SingleOrDefault(x => x.Id == id);
            if (enquiry_info == null)
            {
                return HttpNotFound();
            }
            ViewBag.cid = new SelectList(db.client_info, "id", "firstname", enquiry_info.cid);
            return View(enquiry_info);
        }

        // POST: enquiry_info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(enquiry_info enquiry_info)
        {
            if (ModelState.IsValid)
{
               
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                        attachments_info attachment_info = new attachments_info()
                        {
                            fileName = fileName,
                            cid = enquiry_info.Id

                        };

                       
                        var extension = Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), attachment_info.fileName);
                        file.SaveAs(path);
                        db.Entry(attachment_info).State = EntityState.Added;

                    }
                }

                db.Entry(enquiry_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cid = new SelectList(db.client_info, "id", "firstname", enquiry_info.cid);
            return View(enquiry_info);
        }

        [HttpPost]
        public JsonResult DeleteFile(long? id)
        {
            if (id == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
             //   Guid guid = new Guid(id);
                attachments_info fileDetail = db.attachment.Find(id);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.attachment.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"),fileDetail.fileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public FileResult Downld(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }
        // GET: enquiry_info/Delete/5
        /*   public ActionResult Delete(long? id)
           {
               if (id == null)
               {
                   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               }
               enquiry_info enquiry_info = db.enquiry.Find(id);
               if (enquiry_info == null)
               {
                   return HttpNotFound();
               }
               return View(enquiry_info);
           } */

        // POST: enquiry_info/Delete/5
        /*  [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken] */
        public ActionResult DeleteConfirmed(long id)
        {
            enquiry_info enquiry_info = db.enquiry.Find(id);
            db.enquiry.Remove(enquiry_info);
            var proposal_info = db.proposal.Where(m => m.eid == id);
            var invoice_main = db.proposal.FirstOrDefault(m => m.eid == id);
            db.proposal.RemoveRange(proposal_info);
            var ff= invoice_main == null ? 0 : invoice_main.Id;
            var inv = db.invoice_main.Where(m => m.pid ==ff) ;
            db.invoice_main.RemoveRange(inv);
            db.SaveChanges();
            return RedirectToAction("Index");


        /*    var Order = db.Orders.where(x => x.StoreId == store.StoreId)
var OrderDetail = db.OrderDetails.where(x => x.OrderId == Order.OrderId)
var Product = db.Products.where(x => x.StoreId == store.StoreId)

db.Orders.RemoveRange(Order);
            db.OrderDetails.RemoveRange(OrderDetail);
            db.Products.RemoveRange(Product);
            db.Stores.Remove(store);*/
            
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
