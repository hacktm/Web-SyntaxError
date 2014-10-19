using System.Web;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.Special
{
    public class MySession
    {
        public static MySession Current
        {
            get
            {
                var session =
                    (MySession) HttpContext.Current.Session["__MySession__"];
                if (session != null) return session;
                session = new MySession();
                HttpContext.Current.Session["__MySession__"] = session;
                return session;
            }
        }

        // **** add your session properties here, e.g like this:
        public User UserDetails { get; set; }
    }
}