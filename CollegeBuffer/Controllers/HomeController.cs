using System.Web.Mvc;
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

       
    }
}