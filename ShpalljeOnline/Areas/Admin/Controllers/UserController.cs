using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ShpalljeOnline.Filters;
using ShpalljeOnline.Models;
using ShpalljeOnline.ViewModels;

namespace ShpalljeOnline.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AdminAuthorization]
    public class UserController : Controller
    {
        ShpalljeOnlineDbContext db;

        public UserController()
        {
            db = new ShpalljeOnlineDbContext();
        }

        // GET: Admin/User
        public ActionResult Index(string search = "", string SortColumn = "UserID", string IconClass = "fa-sort-down", int PageNo = 1)
        {
            ViewBag.search = search;
            var users = db.Users.Where(x => (x.Name + " " + x.Surname).StartsWith(search)).ToList();

            /*Sorting*/
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "UserID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    users = users.OrderBy(temp => temp.UserID).ToList();
                else
                    users = users.OrderByDescending(temp => temp.UserID).ToList();
            }
            else if (ViewBag.SortColumn == "Name")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    users = users.OrderBy(temp => temp.Name).ToList();
                else
                    users = users.OrderByDescending(temp => temp.Name).ToList();
            }
            else if (ViewBag.SortColumn == "Surname")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    users = users.OrderBy(temp => temp.Surname).ToList();
                else
                    users = users.OrderByDescending(temp => temp.Surname).ToList();
            }
            else if (ViewBag.SortColumn == "Email")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    users = users.OrderBy(temp => temp.Info.Email).ToList();
                else
                    users = users.OrderByDescending(temp => temp.Info.Email).ToList();
            }
            else if (ViewBag.SortColumn == "PhoneNr")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    users = users.OrderBy(temp => temp.Info.PhoneNr).ToList();
                else
                    users = users.OrderByDescending(temp => temp.Info.PhoneNr).ToList();
            }
            else if (ViewBag.SortColumn == "Role")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    users = users.OrderBy(temp => temp.Role.RoleName).ToList();
                else
                    users = users.OrderByDescending(temp => temp.Role.RoleName).ToList();
            }

            /* Paging */
            int NoOfRecordsPerPage = 10;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(users.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            users = users.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();

            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.roles = db.Roles.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = Crypto.HashPassword(user.Password);
                user.Password = passwordHash;
                User uvm = new User()
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Password = user.Password,
                    RoleID = user.RoleID
                };
                uvm.Info = new Info() { Email = user.Email, PhoneNr = user.PhoneNr };
                db.Users.Add(uvm);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roles = db.Roles.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            ViewBag.roles = db.Roles.ToList();
            UserViewModel uvm = new UserViewModel()
            {
                UserID = user.UserID,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Info.Email,
                PhoneNr = user.Info.PhoneNr,
                RoleID = user.RoleID
            };
            return View(uvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Info.Email = user.Email;
                existingUser.Info.PhoneNr = user.PhoneNr;
                existingUser.RoleID = user.RoleID;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var _user = db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
            ViewBag.roles = db.Roles.ToList();
            return View(user);
            //return RedirectToAction("Edit", new { id = user.UserID });
        }

        public ActionResult Delete(long id)
        {
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}