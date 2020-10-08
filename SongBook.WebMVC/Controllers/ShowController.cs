
using Microsoft.AspNet.Identity;
using SongBook.Data;
using SongBook.Models;
using SongBook.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;

namespace SongBook.WebMVC.Controllers
{
    [Authorize]
    public class ShowController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Show
        public ActionResult Index()
        {
            var service = CreateShowService();
            var model = service.GetShows();

            return View(model);

            
        }

        public ActionResult Create()
        {
            ViewBag.BandId = new SelectList(db.Bands, "BandId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShowCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           var service = CreateShowService();

            service.CreateShow(model);

            return RedirectToAction("Index");
        }
      
        public ActionResult Details (int id)
        {
            var svc = CreateShowService();
            var model = svc.GetShowById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateShowService();
            var detail = service.GetShowById(id);
            var model =
                new ShowEdit
                {
                    ShowId = detail.ShowId,
                    BandId = detail.BandId,
                    Venue = detail.Venue,
                    Location = detail.Location,
                    Date = detail.Date,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ShowEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ShowId != id)
            {
                ModelState.AddModelError("", "Id Does Not Match!");
                return View(model);
            }

            var service = CreateShowService();

            if (service.UpdateShow(model))
            {
                TempData["SaveResult"] = "Show Updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Could Not Update Show!");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateShowService();
            var model = svc.GetShowById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost (int id)
        {
            var service = CreateShowService();
            service.DeleteShow(id);
            TempData["SaveResult"] = "Show Deleted!";
            return RedirectToAction("Index");
        }
        private ShowService CreateShowService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ShowService(userId);
            return service;
        }

            
    }
}

            

