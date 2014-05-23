using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace MarathonSite.Models
{
    public class SearchParser
    {
        private string matchFirstName = "^[A-z]+";
        private string matchLastName = "[A-z]+$";
        private string matchBibNumber = "^[0-9]$";

        public string query
        {
            get;
            set
            {
                query = value;
                firstName = Regex.Match(matchFirstName, query).Value;
                lastName = Regex.Match(matchLastName, query).Value;
                bibNumber = Regex.Match(matchBibNumber, query).Value;
            }
        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string bibNumber { get; set; }
    }
}