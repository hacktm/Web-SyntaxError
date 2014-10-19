using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollegeBuffer.BLL;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.Models;
using CollegeBuffer.Special;

namespace CollegeBuffer.Controllers
{
    public class AnnouncementsController : Controller
    {
        //
        // GET: /Announcements/
        public string New(GroupPage model)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null)
                return "F";

            var db = DbUnitOfWork.NewInstance();
            var myUser = db.UsersRepository.Get(MySession.Current.UserDetails.Id);
            var group = db.GroupsRepository.Get(new Guid(model.GroupId));

            if (group == null) return "F";

            var announcement = new Announcement()
            {
                Group = group,
                Date = DateTime.Now,
                Message = model.NewAnnouncement.Message,
                Title = model.NewAnnouncement.Title,
                User = myUser
            };

            return db.AnnouncementsRepository.Insert(announcement) != null ? "K" : "F";
        }

        public ActionResult Announcement(string id, int asPartial = 0)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null)
                return Content("Invalid Announcement!");

            var db = DbUnitOfWork.NewInstance();
            var announcement = db.AnnouncementsRepository.Get(new Guid(id));

            if (asPartial == 1)
                return PartialView(announcement);
            return View(announcement);
        }
	}
}