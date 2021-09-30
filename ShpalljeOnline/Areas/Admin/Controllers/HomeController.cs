using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShpalljeOnline.Models;
using ShpalljeOnline.Filters;

namespace ShpalljeOnline.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AdminAuthorization]
    public class HomeController : Controller
    {
        ShpalljeOnlineDbContext db;

        public HomeController()
        {
            db = new ShpalljeOnlineDbContext();
        }

        public ActionResult Index(string search = "", long categoryID = 0, long locationID = 0, string sortingOrder = "descending", string fromDate = "", int PageNo = 1)
        {
            ViewBag.search = search;
            ViewBag.categories = db.Categories.ToList();
            ViewBag.selectedCategory = categoryID;
            ViewBag.locations = db.Locations.ToList();
            ViewBag.selectedLocation = locationID;

            if ((string)TempData["deleted"] == "true")
                ViewBag.deletedPost = "true";

            if (fromDate != "")
            {
                ViewBag.selectedSorting = "date";
                ViewBag.selectedDate = fromDate;
            }
            else if (sortingOrder == "ascending")
                ViewBag.selectedSorting = "ascending";
            else if (sortingOrder == "descending")
                ViewBag.selectedSorting = "descending";


            List<Post> posts = db.Posts.ToList();

            /* Filtering */
            if (search != "")
                posts = db.Posts.Where(x => x.Title.Contains(search)).ToList();

            if (categoryID != 0)
                posts = posts.Where(x => x.CategoryID == categoryID).ToList();

            if (locationID != 0)
                posts = posts.Where(x => x.LocationID == locationID).ToList();

            if (fromDate != "")
            {
                posts = posts.Where(x => x.Date.Value.Date >= DateTime.Parse(fromDate).Date).ToList();
            }

            /* Sorting */
            posts = sortingOrder == "ascending" ? posts.OrderBy(x => x.Date).ToList() : posts.OrderByDescending(x => x.Date).ToList();

            /* Paging */
            int NoOfRecordsPerPage = 10;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(posts.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            posts = posts.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();

            return View(posts);
        }
    }
}