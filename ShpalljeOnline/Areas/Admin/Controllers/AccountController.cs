using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShpalljeOnline.ViewModels;
using ShpalljeOnline.Models;
using System.Web.Helpers;
using ShpalljeOnline.Filters;

namespace ShpalljeOnline.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class AccountController : Controller
    {
        ShpalljeOnlineDbContext db;

        public AccountController()
        {
            db = new ShpalljeOnlineDbContext();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [AuthenticationFilter]
        [HttpGet]
        public ActionResult MyProfile()
        {
            long userID = Session["UserID"] != null ? long.Parse(Session["UserID"].ToString()) : 0;
            var user = db.Users.Where(x => x.UserID == userID).FirstOrDefault();

            if (user == null)
                return RedirectToAction("Login", "Account");
            else
            {
                ViewBag.posts = db.Posts.Where(x => x.UserID == userID).ToList();
                UserViewModel uvm = new UserViewModel()
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    Surname = user.Surname,
                    RoleID = user.RoleID,
                    Email = user.Info.Email,
                    PhoneNr = user.Info.PhoneNr
                };
                return View(uvm);
            }
        }

        [AuthenticationFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(UserViewModel user)
        {
            ModelState.Remove("RoleID");
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Info.Email = user.Email;
                existingUser.Info.PhoneNr = user.PhoneNr;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("MyProfile");
            }

            ViewBag.posts = db.Posts.Where(x => x.UserID == user.UserID).ToList();
            UserViewModel uvm = new UserViewModel()
            {
                UserID = user.UserID,
                Name = user.Name,
                Surname = user.Surname,
                RoleID = user.RoleID,
                Email = user.Email,
                PhoneNr = user.PhoneNr
            };
            return View(uvm);
        }

        [AuthenticationFilter]
        public ActionResult ViewProfile(long id)
        {
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            ViewBag.posts = db.Posts.Where(x => x.UserID == id).ToList();
            return View(user);
        }
    }
}