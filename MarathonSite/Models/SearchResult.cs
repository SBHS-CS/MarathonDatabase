using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MarathonSite.Models;

namespace MarathonSite.Models
{
    public class SearchResult
    {
        public string SearchQuery { get; set; }
        public List<Runner> SearchResults { get; set; }

    }
}