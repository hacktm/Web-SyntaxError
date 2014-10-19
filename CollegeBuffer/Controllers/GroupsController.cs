using System;
using System.Linq;
using System.Web.Mvc;
using CollegeBuffer.BLL;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using CollegeBuffer.Models;
using CollegeBuffer.Special;

namespace CollegeBuffer.Controllers
{
    public class GroupsController : Controller
    {

        public ActionResult Index(string id = null, int asPartial = 0)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null)
                return Redirect("/Home/Login");

            using (var db = DbUnitOfWork.NewInstance())
            {
                var model = new Groups();
                Group currentGroup = null;

                if (id == null || (currentGroup = db.GroupsRepository.Get(new Guid(id))) == null)
                    model.ChildGroups = db.GroupsRepository.GetAllSuperGroups();
                else
                {
                    model.ChildGroups = currentGroup.SubGroups.ToArray();
                    var group = currentGroup;
                    while ((group = group.SuperGroup) != null)
                    {
                        model.GroupPath.Insert(0, group);
                    }
                    model.GroupPath.Add(currentGroup);
                }

                if (asPartial == 1)
                    return PartialView(model);
                return View(model);
            }
        }

        public ActionResult Create(string id = null, int asPartial = 0)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null)
                return RedirectPermanent("/Home/Login");

            using (var db = DbUnitOfWork.NewInstance())
            {
                var model = new GroupVM();
                if (id != null)
                {
                    var parentGroup = db.GroupsRepository.Get(new Guid(id));
                    if (parentGroup == null)
                        return RedirectPermanent("/Groups/Index");
                    var myUser = db.UsersRepository.Get(MySession.Current.UserDetails.Id);
                    if (!myUser.GroupsAsAdministrator.Contains(parentGroup) && myUser.Role != UserRoles.Administrator)
                        return RedirectPermanent("/Groups/Index");

                    model.ParentGroup = parentGroup;
                }
                else if (MySession.Current.UserDetails.Role != UserRoles.Administrator)
                {
                    return RedirectPermanent("/Home/Login");
                }

                if (asPartial == 1)
                    return PartialView(model);
                return View(model);
            }
        }

        public string New(GroupVM model)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null)
                return "F";

            using (var db = DbUnitOfWork.NewInstance())
            {
                Group parentGroup = null;
                if (model.ParentGroupId != null)
                {
                    parentGroup = db.GroupsRepository.Get(new Guid(model.ParentGroupId));
                    var myUser = db.UsersRepository.Get(MySession.Current.UserDetails.Id);
                    if (parentGroup == null ||
                        (myUser.GroupsAsAdministrator.FirstOrDefault(p => p.Id == parentGroup.Id) ==
                         null && myUser.Role != UserRoles.Administrator))
                        return "F";
                }
                else if (MySession.Current.UserDetails.Role != UserRoles.Administrator)
                    return "F";

                var group = new Group
                {
                    Description = model.Description,
                    Name = model.Name,
                    SuperGroup = parentGroup,
                };
                var user = db.UsersRepository.Get(MySession.Current.UserDetails.Id);
                group.Administrators.Add(user);

                var insertionReult = db.GroupsRepository.Insert(group);

                return insertionReult != null ? "K" : "F";
            }
        }

        public string Delete(string id)
        {
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            if (MySession.Current.UserDetails == null || id == null)
                return "F";

            using (var db = DbUnitOfWork.NewInstance())
            {
                var group = db.GroupsRepository.Get(new Guid(id));
                var myUser = db.UsersRepository.Get(MySession.Current.UserDetails.Id);
                if (myUser.GroupsAsAdministrator.FirstOrDefault(p => p.Id == group.Id) ==
                     null && myUser.Role != UserRoles.Administrator)
                    return "F";

                return db.GroupsRepository.Delete(group) ? "K" : "F";
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