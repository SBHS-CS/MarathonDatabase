using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarathonSite.Models;

namespace MarathonSite.Controllers
{
    public class RunnerController : Controller
    {
        //
        // GET: /Runner/

        RunnersDBConnectionDataContext db = new RunnersDBConnectionDataContext();

        /*
         * Use @Html.ActionLink("[text to appear on page]", "[method name in controller]", "Runner", [arguments to controller method])
         * To generate links that call controller methods
         * For example, on the search results page, use this method for each
         * runner to call the View method with a Runner object
         */

        /*
         * Make index stronly typed to SearchParser
         * Make search bar a text box for SearchParser's query field (@Html.textBoxFor(m=>m.query))
         * @ViewBag.RunnerCount will give the number of runners in the database
         */
        public ActionResult Index()
        {
            ViewBag.RunnerCount = db.Runners.Count();

            SearchResult sr = new SearchResult();


            return View(sr);
        }

        public ActionResult Get(int id)
        {
            var runner = db.Runners.Where(m => m.Id == id).Single();

            return View(runner);
        }

        //[HttpPost]
        //public ActionResult Index(SearchParser s)
        //{
        //    List<Runner> l;
        //    if (s.firstName != null)
        //    {
        //        if (s.lastName != null)
        //        {
        //            l = (from a in db.Runners
        //                 where a.FirstName == s.firstName && a.LastName == s.lastName
        //                 select a).ToList<Runner>();
        //        }
        //        else
        //        {
        //            l = (from a in db.Runners
        //                 where a.FirstName == s.firstName || a.FirstName == s.lastName
        //                 select a).ToList<Runner>();

        //        }
        //    }
        //    else if (s.bibNumber != null)
        //    {
        //        l = (from a in db.Runners
        //             where a.BibNumber == s.bibNumber
        //             select a).ToList<Runner>();
        //    }
        //    else
        //    {
        //        return RedirectToAction("EmptySearch");
        //    }

        //    return RedirectToAction("SearchResults", l);
        //}

        [HttpPost]
        public ActionResult Index(SearchResult sr)
        {
            if (sr.SearchQuery != null)
            {
                sr.SearchResults = db.Runners.Where(m => m.FirstName.Contains(sr.SearchQuery)).ToList();
            }
            return View("Index", sr);
        }

        //strongly typed to List<Runner>
        //use foreach(Runner r in model) to iterate
        //you can put HTML code in the C# loop to make a table
        public ActionResult SearchResults(List<Runner> l)
        {
            if (l.Count == 0)
            {
                return RedirectToAction("EmptySearch");
            }
            else if (l.Count == 1)
            {
                return RedirectToAction("View", l[0]);
            }
            else
            {
                return View(l);
            }
        }

        //not strongly typed; just say the search didn't match any runners in the databse
        public ActionResult EmptySearch()
        {
            return View();
        }

        //strongly typed to Runner
        //have a button to edit
        //public ActionResult View(Runner r)
        //{
        //    return View(r);
        //}

        //strongly typed to Runner
        //ViewBag.Error contains an error string if they sumbit an invalid Runner
        [HttpPost]
        public ActionResult Edit(Runner r, bool redirect)
        {
            string error = Verification.Verify(r);
            if (error == null)
            {
                if (redirect)
                {
                    return RedirectToAction("View", r);
                }
                else
                {
                    return View(r);
                }
            }
            else
            {
                ViewBag.Error = error;
                return View(r);
            }
        }

        //strongly typed to Runner
        public ActionResult Add()
        {
            return View();
        }

        //ViewBag.Error will contain an error string if they sumbit an invalid Runner
        [HttpPost]
        public ActionResult Add(Runner r)
        {
            string error = Verification.Verify(r);
            if (error == null)
            {
                //add runner to database
                return RedirectToAction("View", r);
            }
            else
            {
                ViewBag.Error = error;
                return View(r);
            }
        }

    }
}
