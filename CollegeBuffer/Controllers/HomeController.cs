﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CollegeBuffer.BLL;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.Models;
using CollegeBuffer.Special;

namespace CollegeBuffer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();

            if (MySession.Current.UserDetails == null)
                return View();

            return Redirect("/Home/Index");
        }

        public ActionResult Index(int asPartial = 0)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null)
                return Redirect("/Home/Login");

            using (var db = DbUnitOfWork.NewInstance())
            {
                var model = new Index();
                var myUser = db.UsersRepository.Get(MySession.Current.UserDetails.Id);

                var announcements = new List<Announcement>();
                var noOfGroups = 0;
                foreach (var group in myUser.GroupsAsAdministrator.Union(myUser.GroupsAsStudent))
                {
                    noOfGroups++;
                    announcements.AddRange(group.Announcements);
                }

                if (noOfGroups == 0)
                {
                    model.Error = "You don't belong to any groups!";
                    if (asPartial == 1)
                        return Content("<h1>" + model.Error + "</h1>");
                    return View(model);
                }


                model.Announcements = announcements;

                if (asPartial == 1)
                    return PartialView(model);
                return View(model);
            }
        }

        public ActionResult MyGroups(int asPartial = 0)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null)
                return Redirect("/Home/Login");

            return Content("");
        }

    }
}