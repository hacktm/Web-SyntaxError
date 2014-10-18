using System;
using System.Web.Mvc;
using CollegeBuffer.BLL;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using CollegeBuffer.Models;
using CollegeBuffer.Special;

namespace CollegeBuffer.Controllers
{
    public class AccountController : Controller
    {
        private readonly CookieHelper _myCookie;

        public AccountController()
        {
            _myCookie = new CookieHelper();
        }

        /// <summary>
        /// Log the user in
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A string value used in javascript checking</returns>
        [HttpPost]
        public string Login(HomePage model)
        {
            using (var db = DbUnitOfWork.NewInstance())
            {
                if (db.UsersRepository.VerifyExists(model.UserName))
                    return "U";

                if (!db.UsersRepository.VerifyPassword(model.UserName, model.Password))
                {
                    return "P";
                }
                
                //Make a new session for the user
                Session newSession = db.SessionsRepository.Login(model.UserName);

                //Create the cookies for the session
                _myCookie.SetCookie("sessionId", newSession.Id.ToString(), DateTime.Now.AddMonths(6));
                _myCookie.SetCookie("sessionKey", newSession.SessionKey, DateTime.Now.AddMonths(6));
            }

            return "K";
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A string value used in javascript checking</returns>
        public string Register(HomePage model)
        {
            using (var db = DbUnitOfWork.NewInstance())
            {
                if (model.NewUserName == null || model.NewUserName.Length < 4 || model.NewUserName.Length > 28 ||
                    !Char.IsLetter(model.NewUserName[0]))
                    return "U";
                if (db.UsersRepository.VerifyExists(model.NewUserName))
                    return "D";
                if (model.EmailAddress == null || !Validations.IsValidEmail(model.EmailAddress))
                    return "E";
                if (model.NewPassword == null || model.NewPassword.Length < 6 || model.NewPassword.Length > 28)
                    return "P";
                if (model.PasswordAgain == null || model.NewPassword != model.PasswordAgain)
                    return "M";

                var newUser = new User
                {
                    Username = model.UserName,
                    EMail = model.EmailAddress,
                    Password = model.Password,
                    Role = UserRoles.Student
                };

                // When creating a new user, the UnitOfWork automatically creates a validation entry in the
                // AccountValidations table for the user.
                // The password is also automatically encrypted in the model
                return db.UsersRepository.Insert(newUser) != null ? "K" : "F";
            }
        }

        /// <summary>
        /// Log out the currently logged in user
        /// </summary>
        /// <returns></returns>
        public string LogOut()
        {
            //Delete the cookies
            _myCookie.DeleteCookie("sessionId");
            _myCookie.DeleteCookie("sessionKey");

            //Delete the session from the server
            try
            {
                //Delete the session variable but remember the userName
                var userName = MySession.Current.UserDetails.UserName;
                MySession.Current.UserDetails = null;

                return DatabaseWorkUnit.Instance.SessionRepository.Logout(userName) ? "K" : "F";
            }
            catch (Exception)
            {
                return "F";
            }
        }

        public AccountModels.UserAccount GetUserDetails()
        {
            if (_myCookie.GetCookie("sessionId") == "" || _myCookie.GetCookie("sessionKey") == "")
                return null;

            var user = DatabaseWorkUnit.Instance.SessionRepository.GetUser(
                new Guid(_myCookie.GetCookie("sessionId")), _myCookie.GetCookie("sessionKey"));

            if (user == null)
            {
                _myCookie.DeleteCookie("sessionId");
                _myCookie.DeleteCookie("sessionKey");
            }

            return user;
        }

    }
}