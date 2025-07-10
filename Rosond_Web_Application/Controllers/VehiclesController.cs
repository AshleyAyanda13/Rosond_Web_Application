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
    public class VehiclesController : Controller
    {
        private MyDbContext db = new MyDbContext();

      
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(v => v.Branch).Include(v => v.Client).Include(v => v.Driver).Include(v => v.Supplier);
            return View(vehicles.ToList());
        }

     
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = db.Vehicles
                .Include(v => v.Supplier)
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
            {
                return HttpNotFound();
            }

            return View(vehicle);
        }

       
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branches, "BranchId", "BranchName");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "CompanyName");
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FullName");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name");
            return View(new Vehicle());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleId,Make,Model,LicensePlate,Year,SupplierId,BranchId,ClientId,DriverId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                bool duplicatePlate = db.Vehicles.Any(v =>
                    v.LicensePlate == vehicle.LicensePlate &&
                    v.VehicleId != vehicle.VehicleId);

                if (duplicatePlate)
                {
                    ModelState.AddModelError("LicensePlate", "This license plate is already registered to another vehicle.");
                }
                else
                {
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Vehicle Record Added successfully!";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.BranchId = new SelectList(db.Branches, "BranchId", "BranchName", vehicle.BranchId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "CompanyName", vehicle.ClientId);
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FullName", vehicle.DriverId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", vehicle.SupplierId);

            return View(vehicle);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = new SelectList(db.Branches, "BranchId", "BranchName", vehicle.BranchId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "CompanyName", vehicle.ClientId);
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FullName", vehicle.DriverId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", vehicle.SupplierId);
            return View(vehicle);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleId,Make,Model,LicensePlate,Year,SupplierId,BranchId,ClientId,DriverId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                bool duplicatePlate = db.Vehicles.Any(v =>
    v.LicensePlate == vehicle.LicensePlate &&
    v.VehicleId != vehicle.VehicleId);

                if (duplicatePlate)
                {
                    ModelState.AddModelError("LicensePlate", "This license plate is already registered to another vehicle.");
                }

                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                TempData["EditMessage"] = "Vehicle Edited successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.BranchId = new SelectList(db.Branches, "BranchId", "BranchName", vehicle.BranchId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "CompanyName", vehicle.ClientId);
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FullName", vehicle.DriverId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", vehicle.SupplierId);
            return View(vehicle);
        }

         
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = db.Vehicles
                .Include(v => v.Supplier)
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
            {
                return HttpNotFound();
            }

            return View(vehicle);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

             
            try
            {
               Vehicle vehicle = db.Vehicles.Find(id);
                if (vehicle != null)
                {
                    db.Vehicles.Remove(vehicle);
                    db.SaveChanges();
                    TempData["DeleteMessage"] = "Vehicle Deleted successfully!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["DeleteError"] = "Unable to delete: This Vehicle is referenced by other records.";
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
