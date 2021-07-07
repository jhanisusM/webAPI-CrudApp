using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlossaryApp.Models;
using System.Net.Http;

namespace GlossaryApp.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            IEnumerable<TermModel> terms;
            var consumeAPI = RESTfulAPI.httpRequest.GetAsync("Glossary").Result;
            terms = consumeAPI.Content.ReadAsAsync<IEnumerable<TermModel>>().Result;
            return View(terms);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new TermModel());
            else
            {
                //GET
                var consumeAPI = RESTfulAPI.httpRequest.GetAsync("Glossary/" + id.ToString()).Result;
                return View(consumeAPI.Content.ReadAsAsync<TermModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(TermModel termModel)
        {
            if (termModel == null)
            {
                TempData["AlertMsg"] = "Please enter a term and its definition.";
                return RedirectToAction("Index");
            }
            if (termModel.TermID == 0)
            {
                //Post
                var consumeAPI = RESTfulAPI.httpRequest.PostAsJsonAsync("Glossary", termModel).Result;
                TempData["AlertMsg"] = consumeAPI.IsSuccessStatusCode ? "Term has been Added." : "Term was not Added.";
            }
            else
            {
                //PUT
                var consumeAPI = RESTfulAPI.httpRequest.PutAsJsonAsync("Glossary/" + termModel.TermID, termModel).Result;
                TempData["AlertMsg"] = consumeAPI.IsSuccessStatusCode ? "Term has been Updated." : "Term was not Updated.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            //Delete
            var consumeAPI = RESTfulAPI.httpRequest.DeleteAsync("Glossary/" + id.ToString()).Result;
            TempData["AlertMsg"] = consumeAPI.IsSuccessStatusCode ? "Term has been Deleted." : "Term was not Deleted.";
            return RedirectToAction("Index");
        }
    }
}
