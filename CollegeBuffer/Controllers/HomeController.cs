using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using CollegeBuffer.BLL;
using CollegeBuffer.DAL.Context;
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

            var db = DbUnitOfWork.NewInstance();

            var model = new Index();
            var myUser = db.UsersRepository.Get(MySession.Current.UserDetails.Id);

            var announcements = new List<Announcement>();
            var events = new List<Event>();
            var noOfGroups = 0;
            var myGroups = myUser.GroupsAsAdministrator.Union(myUser.GroupsAsStudent);
            var groupsToSearch = new Collection<Group>();
            foreach (var group in myGroups)
            {
                var recursiveGroup = group;
                do
                {
                    groupsToSearch.Add(recursiveGroup);
                } while ((recursiveGroup = recursiveGroup.SuperGroup) != null);
            }

            foreach (var group in groupsToSearch.Distinct())
            {
                noOfGroups++;
                announcements.AddRange(group.Announcements.OrderByDescending(p => p.Date).Take(5));
                events.AddRange(group.Events.OrderByDescending(p=>p.DateCreated).Take(5));
            }

            if (noOfGroups == 0)
            {
                model.Error = "You don't belong to any groups!";
                if (asPartial == 1)
                    return Content("<h1>" + model.Error + "</h1>");
                return View(model);
            }

            model.Announcements = announcements;
            model.Events = events;

            if (asPartial == 1)
                return PartialView(model);
            return View(model);

        }

        public ActionResult MyGroups(int asPartial = 0)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null)
                return Redirect("/Home/Login");

            var db = DbUnitOfWork.NewInstance();

            var model = new MyGroupsVM();
            var myUser = db.UsersRepository.Get(MySession.Current.UserDetails.Id);
            model.GroupsAsAdmin = myUser.GroupsAsAdministrator;
            model.GroupsAsUser = myUser.GroupsAsStudent;

            if (asPartial == 1)
                return PartialView(model);
            return View(model);
        }

    }
}