using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace MarathonSite.Models
{
    public class Verification
    {
        private static string lettersOnly = "^[A-z]$+";
        private static string bibNumber = "^[0-9]${5}"; //make sure bib number length is accurate

        public static string Verify(Runner r)
        {
            string result = null;
            bool error = false;
            if (!Regex.IsMatch(lettersOnly, r.FirstName))
            {
                error = true;
                result += " first name,";
            }
            if (!Regex.IsMatch(lettersOnly, r.FirstName))
            {
                error = true;
                result += " last name,";
            }
            if (!Regex.IsMatch(bibNumber, r.BibNumber))
            {
                error = true;
                result += " bib number,";
            }
            if (error)
            {
                result = "Your" + result.Substring(0, result.Length - 1) + " are invalid.";
            }
            return result;
        }
    }
}