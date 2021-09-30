using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using ShpalljeOnline.Models;

namespace ShpalljeOnline
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CreateUsersAndRoles();
        }

        public void CreateUsersAndRoles()
        {
            ShpalljeOnlineDbContext db = new ShpalljeOnlineDbContext();

            if (db.Roles.Where(x => x.RoleName == "Admin").FirstOrDefault() == null)
            {
                Role role = new Role();
                role.RoleName = "Admin";
                db.Roles.Add(role);
                db.SaveChanges();
            }

            if (db.Roles.Where(x => x.RoleName == "User").FirstOrDefault() == null)
            {
                Role role = new Role();
                role.RoleName = "User";
                db.Roles.Add(role);
                db.SaveChanges();
            }

            if (db.Users.Where(x => x.Role.RoleName == "Admin").FirstOrDefault() == null)
            {
                User user = new User();
                user.Name = "Admin";
                user.Surname = "Admin";
                user.RoleID = 1;
                user.Password = Crypto.HashPassword("admin123");

                user.Info = new Info();
                user.Info.Email = "admin@gmail.com";
                user.Info.PhoneNr = "00000000";

                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        //Manage all kinds of exceptions
        protected void Application_Error()
        {
            //Get the last error and save it to string
            Exception ex = Server.GetLastError();
            string s = "Message: " + ex.Message + ", Type: " + ex.GetType().ToString() + ", Source: " + ex.Source;
 
            //Add the last error to logs
            string fullPath = HttpContext.Current.Request.PhysicalApplicationPath + "\\ErrorLog.txt";
            File.WriteAllText(fullPath,s);

            //Redirect to error page
            Response.Redirect("/Home/ManageException");
        }
    }
}
