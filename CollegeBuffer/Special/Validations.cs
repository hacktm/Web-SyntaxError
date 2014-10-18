using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeBuffer.Special
{
    public static class Validations
    {
        /// <summary>
        /// Test if the specified email is valid
        /// </summary>
        /// <param name="email">The email string to be tested</param>
        /// <returns>Whether the email is valid or not</returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                new System.Net.Mail.MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}