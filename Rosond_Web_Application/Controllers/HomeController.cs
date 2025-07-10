using Rosond_Web_Application.Data;
using Rosond_Web_Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class HomeController : Controller
{
    private MyDbContext db = new MyDbContext();

    public ActionResult Index()
    {

        var supplierGroups = db.Vehicles
            .GroupBy(v => new { v.Make, SupplierName = v.Supplier.Name })
            .Select(g => new ReportRow
            {
                Group = "Supplier",
                Category = g.Key.SupplierName,
                Manufacturer = g.Key.Make,
                Count = g.Count()
            });

      
        var branchGroups = db.Vehicles
            .GroupBy(v => new { v.Make, BranchName = v.Branch.BranchName })
            .Select(g => new ReportRow
            {
                Group = "Branch",
                Category = g.Key.BranchName,
                Manufacturer = g.Key.Make,
                Count = g.Count()
            });

  
        var clientGroups = db.Vehicles
            .GroupBy(v => new { v.Make, ClientName = v.Client.CompanyName })
            .Select(g => new ReportRow
            {
                Group = "Client",
                Category = g.Key.ClientName,
                Manufacturer = g.Key.Make,
                Count = g.Count()
            });

    
        var totalGroups = db.Vehicles
            .GroupBy(v => v.Make)
            .Select(g => new ReportRow
            {
                Group = "Total",
                Category = "All",
                Manufacturer = g.Key,
                Count = g.Count()
            });

     
        var allGroups = supplierGroups
            .Concat(branchGroups)
            .Concat(clientGroups)
            .Concat(totalGroups)
            .ToList();

     
        return View(allGroups);
    }
}
