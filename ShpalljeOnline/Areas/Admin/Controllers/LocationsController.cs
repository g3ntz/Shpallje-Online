using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShpalljeOnline.Filters;
using ShpalljeOnline.Models;

namespace ShpalljeOnline.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AdminAuthorization]
    public class LocationsController : Controller
    {
        ShpalljeOnlineDbContext db;

        public LocationsController()
        {
            db = new ShpalljeOnlineDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Locations = db.Locations.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Locations = db.Locations.ToList();
            return View();
        }


        public ActionResult Delete(long id)
        {
            var location = db.Locations.Where(x => x.LocationID == id).FirstOrDefault();
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}