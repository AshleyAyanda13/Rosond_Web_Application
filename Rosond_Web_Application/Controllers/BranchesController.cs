using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rosond_Web_Application.Data;
using Rosond_Web_Application.Models;

namespace Rosond_Web_Application.Controllers
{
    public class BranchesController : Controller
    {
        private MyDbContext db = new MyDbContext();

      
        public ActionResult Index()
        {
            return View(db.Branches.ToList());
        }
 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        
        public ActionResult Create()
        {
            return View();
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BranchId,BranchName,Location,ManagerName,PhoneNumber,Email")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Branch Record Added successfully!";
                return RedirectToAction("Index");
            }

            return View(branch);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BranchId,BranchName,Location,ManagerName,PhoneNumber,Email")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                TempData["EditMessage"] = "Branch Edited successfully!";
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
       
           


            try
            {
               Branch branch = db.Branches.Find(id);
                if (branch != null)
                {
                    db.Branches.Remove(branch);
                    db.SaveChanges();
                    TempData["DeleteMessage"] = "Branch Deleted successfully!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["DeleteError"] = "Unable to delete: This branch is referenced by other records.";
                return RedirectToAction("Index");
            }
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
