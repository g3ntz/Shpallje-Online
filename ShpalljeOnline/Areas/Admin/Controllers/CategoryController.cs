using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShpalljeOnline.Areas.Admin;
using ShpalljeOnline.Filters;
using ShpalljeOnline.Models;


namespace ShpalljeOnline.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AdminAuthorization]
    public class CategoryController : Controller
    {
        ShpalljeOnlineDbContext db;

        public CategoryController()
        {
            db = new ShpalljeOnlineDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        public ActionResult Delete(long id)
        {
            var category = db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}