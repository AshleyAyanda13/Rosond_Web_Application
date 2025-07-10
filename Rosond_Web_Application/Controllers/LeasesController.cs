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
    public class LeasesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        public ActionResult Browse(string searchTerm)
        {
            var leases = db.Leases
                .Include(l => l.Client)
                .Include(l => l.Driver)
                .Include(l => l.Vehicle)
                .Include(l => l.Vehicle.Supplier)
                .Include(l => l.Vehicle.Branch);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();

                leases = leases.Where(l =>
                    l.Client.CompanyName.ToLower().Contains(term) ||
                    l.Driver.FullName.ToLower().Contains(term) ||
                    l.Vehicle.Make.ToLower().Contains(term) ||
                    l.Vehicle.LicensePlate.ToLower().Contains(term) ||
                    l.Vehicle.Supplier.Name.ToLower().Contains(term) ||
                    l.Vehicle.Branch.BranchName.ToLower().Contains(term)
                );
            }

            return View(leases);
        }

        // GET: Leases
        public ActionResult Index()
        {
            var leases = db.Leases
                .Include(l => l.Client)
                .Include(l => l.Driver)
                .Include(l => l.Vehicle)
                .Include(l => l.Vehicle.Supplier)
                .Include(l => l.Vehicle.Branch);


            
            return View(leases);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lease leasee = db.Leases.Find(id);
            if (leasee == null)
            {
                return HttpNotFound();
            }
            Lease lease = db.Leases
    .Include(l => l.Client)
    .Include(l => l.Driver)
    .Include(l => l.Vehicle)
    .Include(l => l.Vehicle.Supplier)
    .Include(l => l.Vehicle.Branch)
    .FirstOrDefault(l => l.LeaseId == id);
            return View(lease);
        }

      
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "CompanyName");
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FullName");
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "Make");
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaseId,LeaseStartDate,LeaseEndDate,LeaseAmount,VehicleId,ClientId,DriverId")] Lease lease)
        {
            if (ModelState.IsValid)
            {
                if (lease.LeaseStartDate >= lease.LeaseEndDate)
                {
                    ModelState.AddModelError("", "Lease start date must be earlier than end date.");
                }
                else
                {
                    db.Leases.Add(lease);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Lease Record Added successfully!";
                    return RedirectToAction("Index");
                }
            }
            
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "CompanyName", lease.ClientId);
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FullName", lease.DriverId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "Make", lease.VehicleId);
            return View(lease);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lease lease = db.Leases.Find(id);
            if (lease == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "CompanyName", lease.ClientId);
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FullName", lease.DriverId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "Make", lease.VehicleId);
            return View(lease);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaseId,LeaseStartDate,LeaseEndDate,LeaseAmount,VehicleId,ClientId,DriverId")] Lease lease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lease).State = EntityState.Modified;
                db.SaveChanges();
                TempData["EditMessage"] = "Lease Edited successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "CompanyName", lease.ClientId);
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FullName", lease.DriverId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "Make", lease.VehicleId);
            return View(lease);
        }

       
        public ActionResult Delete(int? id)
        {
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lease leasee = db.Leases.Find(id);
            if (leasee == null)
            {
                return HttpNotFound();
            }
            Lease lease = db.Leases
    .Include(l => l.Client)
    .Include(l => l.Driver)
    .Include(l => l.Vehicle)
    .Include(l => l.Vehicle.Supplier)
    .Include(l => l.Vehicle.Branch)
    .FirstOrDefault(l => l.LeaseId == id);
            return View(lease);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lease lease = db.Leases.Find(id);
            db.Leases.Remove(lease);
            db.SaveChanges();
            TempData["DeleteMessage"] = "Lease Deleted successfully!";
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
