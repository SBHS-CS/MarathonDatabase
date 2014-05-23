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

        //databse connection called db

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchParser s)
        {
            List<Runner> l;
            if (s.firstName != null)
            {
                if (s.lastName != null)
                {
                    l = (from a in runners
                         where a.firstName == s.firstName && a.lastName == s.lastName
                         select a).toList();
                }
                else
                {
                    l = (from a in runners
                         where a.firstName == s.firstName || a.firstName == s.lastName
                         select a).toList();
                }
            }
            else if (s.bibNumber != null)
            {
                l = (from a in runners
                     where a.bibNumber == s.bibNumber
                     select a).toList();
            }
            else
            {
                return RedirectToAction("EmptySearch");
            }

            return RedirectToAction("SearchResults", l);
        }

        public ActionResult SearchResults(List<Runner> l)
        {
            if (l.Count == 0)
            {
                return RedirectToAction("EmptySearch");
            }
            else if (l.Count == 1)
            {
                return RedirectToAction("View", l[0]);
            } else {
                return View(l);
            }
        }

        public ActionResult EmptySearch()
        {
            return View();
        }

        public ActionResult View(Runner r)
        {
            return View(r);
        }

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
            } else {
                ViewBag.Error = error;
                return View(r);
            }
        }

        public ActionResult Add()
        {
            return View();
        }

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
