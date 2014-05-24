using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MarathonSite.Models;
using System.ComponentModel;

namespace MarathonSite.Models
{
    public class SearchResult
    {
        [DisplayName("Search")]
        public string SearchQuery { get; set; }

        [DisplayName("Search Results")]
        public List<Runner> SearchResults { get; set; }

        public SearchResult()
        {
            this.SearchQuery = "";
            this.SearchResults = new List<Runner>();
        }
    }
}