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
    public class PostController : Controller
    {
        ShpalljeOnlineDbContext db;

        public PostController()
        {
            db = new ShpalljeOnlineDbContext();
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Show a message to the view when a post is created
            if (TempData["justCreated"] != null && (bool)TempData["justCreated"] == true)
                ViewBag.justCreated = true;
            // send the created post id to view the full infos of that post
            if (TempData["postID"] != null)
                ViewBag.postID = TempData["postID"].ToString();

            ViewBag.categories = db.Categories.ToList();
            ViewBag.locations = db.Locations.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    var imgBytes = new byte[file.ContentLength - 1];
                    file.InputStream.Read(imgBytes, 0, imgBytes.Length);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    post.Photo = base64String;
                }

                var email = Session["Email"].ToString();
                if (post.Info == null)
                {
                    var infosID = db.Users.Where(x => x.Info.Email == email).Select(x => x.InfoID).FirstOrDefault();
                    post.InfoID = infosID;
                }

                post.Date = DateTime.Now;
                post.UserID = long.Parse(Session["UserID"].ToString());
                db.Posts.Add(post);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                TempData["justCreated"] = true;
                TempData["postID"] = db.Posts.Select(x => x.PostID).Max();

                return RedirectToAction("Create");
            }
            ViewBag.categories = db.Categories.ToList();
            ViewBag.locations = db.Locations.ToList();
            return View();
        }


        [HttpGet]
        public ActionResult Details(long id)
        {
            var post = db.Posts.Where(x => x.PostID == id).FirstOrDefault();
            if (Session["UserID"] != null)
                ViewBag.UserID = long.Parse(Session["UserID"].ToString());
            return View(post);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var post = db.Posts.Where(x => x.PostID == id).FirstOrDefault();
            ViewBag.categories = db.Categories.ToList();
            ViewBag.locations = db.Locations.ToList();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                var existingPost = db.Posts.Where(x => x.PostID == post.PostID).FirstOrDefault();

                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    var imgBytes = new byte[file.ContentLength - 1];
                    file.InputStream.Read(imgBytes, 0, imgBytes.Length);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    post.Photo = base64String;
                    existingPost.Photo = post.Photo;
                }

                existingPost.Title = post.Title;
                existingPost.Description = post.Description;
                existingPost.CategoryID = post.CategoryID;
                existingPost.LocationID = post.LocationID;

                if (post.Info != null)
                {
                    var currentUser = db.Users.Where(x => x.UserID == existingPost.UserID).FirstOrDefault();
                    if (currentUser.InfoID != existingPost.InfoID)
                    {
                        var existingInfo = db.Infos.Where(x => x.InfoID == existingPost.InfoID).FirstOrDefault();
                        existingInfo.PhoneNr = post.Info.PhoneNr;
                        existingInfo.Email = post.Info.Email;
                    }
                    else
                    {
                        existingPost.Info = post.Info;
                    }
                    db.SaveChanges();
                }

                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = post.PostID });
            }
            ViewBag.categories = db.Categories.ToList();
            ViewBag.locations = db.Locations.ToList();
            return View(post);
        }

        [HttpPost]
        public ActionResult Delete(long id, string deletedFrom)
        {
            var post = db.Posts.Where(x => x.PostID == id).FirstOrDefault();
            var userID = post.UserID;

            if (post != null)
            {
                var userInfoID = db.Users.Where(x => x.UserID == userID).Select(x => x.InfoID).FirstOrDefault();
                if (post.InfoID != userInfoID)
                {
                    var info = db.Infos.Where(x => x.InfoID == post.InfoID).FirstOrDefault();
                    db.Infos.Remove(info);
                    db.SaveChanges();
                }
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            // If post is deleted from my profile then redirect to my profile
            // if post is deleted from post details then redirect to home
            // if post is deleted from view profile then redirect to view profile
            if (deletedFrom == null)
            {
                TempData["deleted"] = "true";
                return RedirectToAction("Index", "Home");
            }
            else if(deletedFrom == "MyProfile")
                return RedirectToAction("MyProfile", "Account", new { id = userID });
            else
                return RedirectToAction("ViewProfile", "Account", new { id = userID });
        }
    }
}