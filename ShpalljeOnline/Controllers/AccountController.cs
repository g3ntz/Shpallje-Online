using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShpalljeOnline.ViewModels;
using ShpalljeOnline.Models;
using System.Web.Helpers;
using ShpalljeOnline.Filters;

namespace ShpalljeOnline.Controllers
{
    public class AccountController : Controller
    {
        ShpalljeOnlineDbContext db;

        public AccountController()
        {
            db = new ShpalljeOnlineDbContext();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = Crypto.HashPassword(rvm.Password);
                User user = new User() { Name = rvm.Name, Surname = rvm.Surname, Password = passwordHash, RoleID = 2 };
                user.Info = new Info() { Email = rvm.Email, PhoneNr = rvm.PhoneNr };
                db.Users.Add(user);
                db.SaveChanges();

                Session["UserID"] = db.Users.Select(x => x.UserID).Max();
                Session["Name"] = rvm.Name;
                Session["Surname"] = rvm.Surname;
                Session["Email"] = rvm.Email;
                Session["PhoneNr"] = rvm.PhoneNr;
                Session["Role"] = "User";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(x => x.Info.Email == lvm.Email).FirstOrDefault();
                if (user != null)
                {
                    var isValidPassword = Crypto.VerifyHashedPassword(user.Password, lvm.Password);
                    if (isValidPassword)
                    {
                        Session["UserID"] = user.UserID;
                        Session["Name"] = user.Name;
                        Session["Surname"] = user.Surname;
                        Session["Email"] = user.Info.Email;
                        Session["PhoneNr"] = user.Info.PhoneNr;
                        Session["Role"] = user.Role.RoleName;

                        if (user.Role.RoleName == "Admin")
                            return RedirectToRoute(new { area = "admin", controller = "Home", action = "Index" });
                        else
                            return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.InvalidData = true;
            return View();
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

        public ActionResult NotAuthorized()
        {
            return View("~/Views/Shared/NotAuthorized.cshtml");
        }
    }
}