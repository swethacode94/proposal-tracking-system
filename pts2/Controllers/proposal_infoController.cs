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
    public class proposal_infoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: proposal_info
        public ActionResult Index()
        {
            var proposal = db.proposal.Include(p => p.enquiry);
            return View(proposal.ToList());
        }

        
        [HttpPost]
        public ActionResult AutoComplete(string term)
        {
            var result = (from r in db.enquiry
                          join t in db.client_info on r.cid equals t.id
                          where r.projectname.ToLower().Contains(term.ToLower())
                          select new { r.projectname, t.email_id, r.Id });

          
        
          
            return Json(result, JsonRequestBehavior.AllowGet);
            
           


        } 

        [HttpPost]
        public ActionResult AutoCompletesent(string term)
        {

            var result = (from r in db.client_info
                          where r.email_id.ToLower().Contains(term.ToLower())
                          select new { r.email_id, r.firstname });

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        

        // GET: proposal_info/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proposal_info proposal_info = db.proposal.Find(id);
            if (proposal_info == null)
            {
                return HttpNotFound();
            }
            return View(proposal_info);
        }   

        // GET: proposal_info/Create
        public ActionResult Create()
        {
            ViewBag.eid = new SelectList(db.enquiry, "Id", "projectname");
            return View();
        }

        // POST: proposal_info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,eid,prop_enquiry,sentdate,sent_to,sent_via")] proposal_info proposal_info )
        {
           

            if(ModelState.IsValid)
            {
             /*   var result = (from r in db.enquiry
                              join t in db.client_info on r.cid equals t.id
                              where r.projectname.ToLower().Contains(term.ToLower())
                              select new { r.projectname, t.email_id, r.Id });

                 return Json(result, JsonRequestBehavior.AllowGet); */

              //  if (userexist != null)
               // {

                    List<pros_attachments_info> attachment = new List<pros_attachments_info>();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                            pros_attachments_info attachment_info = new pros_attachments_info()
                            {
                                fileName = fileName


                            };

                            attachment.Add(attachment_info);
                            var extension = Path.GetExtension(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data/PUpload/"), attachment_info.fileName);
                            file.SaveAs(path);

                        }
                    }

            /*    var q = from c in db.enquiry where c.projectname == proposal_info.prop_enquiry select c;
                
                if (q != null)
                   {
                     foreach (var cust in q)
                      {
                          proposal_info.eid = cust.Id;
                      }
                   }
                else
                   {
                     proposal_info.eid = null;
                  } */
                    if (proposal_info.eid != null)
                    {
                        enquiry_info e_info = db.enquiry.Find(proposal_info.eid);

                        e_info.status = Status.Proposal;
                        db.Entry(e_info).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    }
                    
                    db.proposal.Add(proposal_info);
                    proposal_info.pros_attachments = attachment;
                    db.SaveChanges();

                    return RedirectToAction("Index");
              //  }

              /*  else
                {
                    ModelState.AddModelError("", "project doesnot exists");

                    return View(proposal_info);

                }*/
            }



            ViewBag.cid = new SelectList(db.enquiry, "id", "projectname", proposal_info.eid);

            return View(proposal_info);

          /*  if (ModelState.IsValid)
            {
                db.proposal.Add(proposal_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.eid = new SelectList(db.enquiry, "Id", "projectname", proposal_info.eid);
            return View(proposal_info); */
        }

        public void Download(long id)
        {
            /* attachments_info attachment;
            
                attachment = db.attachment.FirstOrDefault(x => x.cid == id);
 
         
                    return File("~/App_Data/Upload/" + attachment.fileName, "application/force-download", Path.GetFileName(attachment.fileName));
            
                 */

            string zipfilename = Guid.NewGuid() + "MyZip.zip";
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", zipfilename);
            byte[] buffer = new byte[1000000];
            ZipOutputStream zipoutputstream = new ZipOutputStream(Response.OutputStream);
            zipoutputstream.SetLevel(3);
            try
            {

                // attachments_info attachment;

                //  attachment = db.attachment.FirstOrDefault(x => x.cid == id);

                var q = from c in db.pros_attachment where c.pid== id select c;




                DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/App_Data/PUpload/"));


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

        // GET: proposal_info/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
              proposal_info proposal_info = db.proposal.Include(s => s.pros_attachments).SingleOrDefault(x => x.Id == id);

            if (proposal_info == null)
            {
                return HttpNotFound();
            }
            ViewBag.eid = new SelectList(db.enquiry, "Id", "projectname", proposal_info.eid);
            return View(proposal_info);
        }

        // POST: proposal_info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( proposal_info proposal_info)
        {
            if (ModelState.IsValid)
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                        pros_attachments_info attachment_info = new pros_attachments_info()
                        {
                            fileName = fileName,

                            pid = proposal_info.Id
                        };

                       
                        var extension = Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/PUpload/"), attachment_info.fileName);
                        file.SaveAs(path);
                        db.Entry(attachment_info).State = EntityState.Added;

                    }
                }




                db.Entry(proposal_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.eid = new SelectList(db.enquiry, "Id", "projectname", proposal_info.eid);
            return View(proposal_info);
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
               pros_attachments_info fileDetail = db.pros_attachment.Find(id);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.pros_attachment.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/PUpload/"), fileDetail.fileName);
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
            return File(Path.Combine(Server.MapPath("~/App_Data/PUpload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }


        // GET: proposal_info/Delete/5
        /*  public ActionResult Delete(long? id)
          {
              if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              proposal_info proposal_info = db.proposal.Find(id);
              if (proposal_info == null)
              {
                  return HttpNotFound();
              }
              return View(proposal_info);
          }

          // POST: proposal_info/Delete/5
          [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken] */

        public ActionResult DeleteConfirmed(long id)
        {
            proposal_info proposal_info = db.proposal.Find(id);
            db.proposal.Remove(proposal_info);
            var invoice_main = db.invoice_main.Where(m => m.pid == proposal_info.Id);
            db.invoice_main.RemoveRange(invoice_main);
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
