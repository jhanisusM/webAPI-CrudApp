using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlossaryApp.Models;
using System.Net.Http;

namespace GlossaryApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<TermModel> terms;
            var response = RESTfulAPI.httpRequest.GetAsync("Glossary").Result;
            terms = response.Content.ReadAsAsync<IEnumerable<TermModel>>().Result;
            ViewBag.Title = "GlossaryAPP";
         
            return View(terms);
        }
    }
}
