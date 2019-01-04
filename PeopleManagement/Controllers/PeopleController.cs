using PeopleManagement.Domain;
using PeopleManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeopleManagement.Controllers
{
    public class PeopleController : Controller
    {
        private IPeopleMangementService peopleManagementService;
        public PeopleController(IPeopleMangementService peopleManagementService)
        {
            this.peopleManagementService = peopleManagementService;
        }

        // GET: People
        public ActionResult Index()
        {
            List<State> states = peopleManagementService.GetStates();
            
            ViewBag.States = new SelectList(states, "stateId", "code"); 
            return View();
        }

        public ActionResult GetPeople(Person person)
        {
            var people = peopleManagementService.GetPeople(person);
            
            return Json(people, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upsert(Person person)
        {
            var result = peopleManagementService.UpertPerson(person);
             return Content(result.ToString());
        }


        // POST: People/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: People/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: People/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
